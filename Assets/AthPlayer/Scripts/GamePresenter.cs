using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace AthPlayer {
  public delegate void OnLocationHovered(Location location);
  public delegate void OnLocationClicked(Location location);

  public delegate void IEffectStaller(long untilTimeMs);

  public class GamePresenter : IGameEffectVisitor, IGameEffectObserver, IUnitMutSetEffectVisitor, IUnitMutSetEffectObserver, IGameEventVisitor, ICommMutListEffectObserver, ICommMutListEffectVisitor {
    public delegate void OnExitClicked();

    public event OnExitClicked exitClicked;
    public OnLocationHovered LocationHovered;
    public OnLocationClicked LocationClicked;

    SlowableTimerClock timer;
    SlowableTimerClock cinematicTimer;
    SoundPlayer soundPlayer;

    GameObject thinkingIndicator;
    SuperstructureWrapper serverSS;
    EffectBroadcaster previewBroadcaster;
    EffectBroadcaster broadcaster;
    Game game;
    Level viewedLevel;
    Instantiator instantiator;
    CameraController cameraController;

    InputSemaphore inputSemaphore;

    TerrainPresenter terrainPresenter;
    OverlayPaneler overlayPaneler;
    OverlayPresenterFactory overlayPresenterFactory;
    ShowError showError;
    ShowInstructions showInstructions;

    Dictionary<int, UnitPresenter> unitPresenters;
    List<KeyValuePair<long, UnitView>> doomedUnitViews;

    PlayerController playerController;

    OverlayPresenter currentModalOverlay;

    long stallEffectUntilTimeMs;

    CommEffectStaller commEffectStaller;

    public GamePresenter(
        ITimer cameraTimer,
        SoundPlayer soundPlayer,
        GameObject thinkingIndicator,
        IncendianFalls.Superstructure innerServerSS,
        InputSemaphore inputSemaphore,
        Instantiator instantiator,
        OverlayPresenterFactory overlayPresenterFactory,
        ShowError showError,
        ShowInstructions showInstructions,
        CameraController cameraController,
        GameObject stalledIndicator,
        LookPanelView lookPanelView,
        OverlayPaneler overlayPaneler,
        int randomSeed,
        int startLevel) {
      this.soundPlayer = soundPlayer;
      this.serverSS = new SuperstructureWrapper(innerServerSS);
      this.inputSemaphore = inputSemaphore;
      this.instantiator = instantiator;
      this.overlayPresenterFactory = overlayPresenterFactory;
      this.overlayPaneler = overlayPaneler;
      this.showError = showError;
      this.showInstructions = showInstructions;
      this.thinkingIndicator = thinkingIndicator;
      this.cameraController = cameraController;
      this.doomedUnitViews = new List<KeyValuePair<long, UnitView>>();

      Debug.Log("Random seed: " + randomSeed);
      //Debug.LogWarning("Hardcoding random seed!");
      //var randomSeed = 1525224206;
      //var game = ss.RequestSetupIncendianFallsGame(randomSeed, false);

      // Immediately getting ID of the server-game. We dont want to keep the server-game
      // around, we want to deal with the client game.
      var gameId = serverSS.RequestSetupEmberDeepGame(randomSeed, startLevel, false).id;

      Root clientRoot = new Root(new LoggerImpl());
      var (appliedEffects, _) =
        clientRoot.Transact(() => {
          // Consume all effects until we have a game.
          while (serverSS.waitingEffects.Count > 0) {
            var effect = serverSS.waitingEffects.Dequeue();
            new EffectApplier(clientRoot).Apply(effect);
            if (clientRoot.GameExists(gameId) && clientRoot.GetGame(gameId).level.Exists()) {
              break;
            }
          }
          return 0;
        });
      Asserts.Assert(clientRoot.GameExists(gameId));
      game = clientRoot.GetGame(gameId);

      previewBroadcaster = new EffectBroadcaster();
      broadcaster = new EffectBroadcaster();
      stallEffectUntilTimeMs = 0;

      var looker = new Looker(lookPanelView, broadcaster);

      timer = new SlowableTimerClock(1f);
      cinematicTimer = new SlowableTimerClock(1f);

      commEffectStaller = new CommEffectStaller(timer, this, previewBroadcaster, StallEffect);

      inputSemaphore.OnLocked += () => timer.SetTimeSpeedMultiplier(0f);
      inputSemaphore.OnUnlocked += () => timer.SetTimeSpeedMultiplier(1f);

      game.AddObserver(broadcaster, this);
      game.comms.AddObserver(broadcaster, this);

      playerController =
          new PlayerController(
              timer,
              cinematicTimer,
              inputSemaphore,
              serverSS,
              broadcaster,
              game,
              looker,
              overlayPaneler,
              overlayPresenterFactory,
              cameraController,
              showInstructions,
              showError,
              thinkingIndicator);

      LoadLevel();
    }

    private List<IEffect> GetNextChunkOfReadyEffects() {
      var readyEffects = new List<IEffect>();
      if (serverSS.waitingEffects.Count == 0) {
        return readyEffects;
      }
      if (timer.GetTimeMs() < stallEffectUntilTimeMs) {
        Debug.Log("stalled!");
        return readyEffects;
      }

      while (serverSS.waitingEffects.Count > 0) {
        var effect = serverSS.waitingEffects.Peek();

        previewBroadcaster.Broadcast(effect);
        // The above previewBroadcaster should make interested parties set the stallEffectUntilTimeMs.
        if (timer.GetTimeMs() < stallEffectUntilTimeMs) {
          break;
        }
        readyEffects.Add(serverSS.waitingEffects.Dequeue());
        // If the last one was an actionNum getting set, then break here, that's the end of the chunk.
        // we should get the next waiting chunk in the same frame, unless an animation stalls it.
        if (effect is GameSetActionNumEffect) {
          break;
        }
      }

      Debug.Log("returning " + readyEffects.Count + " ready effects!");
      return readyEffects;
    }

    private void ConsumeEffects() {
      float startUnityTime = Time.time;
      while (true) {
        if (Time.time - startUnityTime > .2f) {
          Asserts.Assert(false, "Consuming effects took too long!");
          break;
        }

        var readyEffects = GetNextChunkOfReadyEffects();

        if (readyEffects.Count > 0) {
          game.root.Transact(() => {
            foreach (var effect in readyEffects) {
              //Debug.Log("Applying " + effect);
              new EffectApplier(game.root).Apply(effect);
              broadcaster.Broadcast(effect);
            }
            return 0;
          });
        }

        // At this point, game is caught up to server game.
        if (serverSS.waitingEffects.Count == 0 && !game.WaitingOnPlayerInput()) {
          var error = serverSS.RequestResume(game.id);
          Asserts.Assert(error == "", error);
          continue;
        } else {
          break;
        }
      }
    }

    public TerrainPresenter GetTerrainPresenter() { return terrainPresenter; }
    public UnitPresenter GetPlayerPresenter() {
      return unitPresenters[game.player.id];
    }

    void RemoveUnit(int unitId) {
      var unit = game.root.GetUnit(unitId);
      if (game.player.Exists()) {
        if (unit.NullableIs(game.player)) {
          return;
        }
      }
      var (animationsEndTime, unitView) =
        this.unitPresenters[unitId].DestroyUnitPresenter();
      this.unitPresenters.Remove(unitId);

      if (unitView) {
        doomedUnitViews.Add(new KeyValuePair<long, UnitView>(animationsEndTime, unitView));
      }
    }

    void AddUnit(int unitId) {
      var unit = game.root.GetUnit(unitId);
      if (game.player.Exists()) {
        if (unit.NullableIs(game.player)) {
          if (unitPresenters.ContainsKey(unitId)) {
            return;
          }
        }
      }
      unitPresenters[unitId] =
          new UnitPresenter(
              timer, timer, soundPlayer, previewBroadcaster, StallEffect, broadcaster, game, viewedLevel.terrain, unit, instantiator);
    }

    private void StallEffect(long untilTimeMs) {
      stallEffectUntilTimeMs = Math.Max(stallEffectUntilTimeMs, untilTimeMs);
    }

    private void LoadLevel() {
      viewedLevel = game.level;

      this.terrainPresenter = new TerrainPresenter(timer, timer, broadcaster, viewedLevel.terrain, instantiator);
      terrainPresenter.TerrainClicked += (location) => LocationClicked?.Invoke(location);
      terrainPresenter.TerrainHovered += (location) => LocationHovered?.Invoke(location);

      viewedLevel.units.AddObserver(broadcaster, this);
      this.unitPresenters = new Dictionary<int, UnitPresenter>();
      foreach (var unit in viewedLevel.units) {
        AddUnit(unit.id);
      }

      playerController.OnLevelLoaded();
    }

    private void UnloadLevel() {
      foreach (var entry in doomedUnitViews) {
        entry.Value.Destruct();
      }
      doomedUnitViews.Clear();

      foreach (var entry in unitPresenters) {
        var (animationsEndTime, unitView) = entry.Value.DestroyUnitPresenter();
        // Dont care about animation end times, kill it right now.
        if (unitView != null) {
          unitView.Destruct();
        }
      }
      unitPresenters.Clear();
      this.unitPresenters = null;

      // Could have been destroyed in a revert.
      if (viewedLevel.Exists()) {
        viewedLevel.units.RemoveObserver(broadcaster, this);
      }

      terrainPresenter.DestroyTerrainPresenter();
      terrainPresenter = null;

      viewedLevel = null;
    }

    public void OnGameEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) {
      UnloadLevel();
      LoadLevel();
    }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetHideInputEffect(GameSetHideInputEffect effect) { }
    public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) {
      showInstructions(effect.newValue);
    }

    public void visitUnitMutSetCreateEffect(UnitMutSetCreateEffect effect) { }
    public void visitUnitMutSetDeleteEffect(UnitMutSetDeleteEffect effect) { }
    public void visitUnitMutSetAddEffect(UnitMutSetAddEffect effect) {
      AddUnit(effect.element);
    }
    public void visitUnitMutSetRemoveEffect(UnitMutSetRemoveEffect effect) {
      RemoveUnit(effect.element);
    }
    public void OnUnitMutSetEffect(IUnitMutSetEffect effect) { effect.visitIUnitMutSetEffect(this); }

    public Location LocationFor(GameObject obj) {
      Location location = terrainPresenter.LocationFor(obj);
      //if (location == null) {
      //  location = LocationForUnit(hit.collider.gameObject);
      //}
      return location;
    }

    public Unit UnitAtLocation(Location location) {
      foreach (var unitPresenter in unitPresenters) {
        if (unitPresenter.Value.unit.Exists()) {
          if (unitPresenter.Value.unit.location == location) {
            return unitPresenter.Value.unit;
          }
        }
      }
      return Unit.Null;
    }

    public TerrainTile TileAtLocation(Location location) {
      if (game.level.terrain.tiles.ContainsKey(location)) {
        return game.level.terrain.tiles[location];
      } else {
        return TerrainTile.Null;
      }
    }

    public void SetHighlightedLocation(Location location) {
      terrainPresenter.SetHighlightLocation(location);
    }

    //private Location LocationForUnit(GameObject gameObject) {
    //  var mousedUnitPresenterComponent = gameObject.GetComponentInParent<UnitPresenterComponent>();
    //  if (mousedUnitPresenterComponent) {
    //    return mousedUnitPresenterComponent.presenter.unit.location;
    //  }
    //  return null;
    //}



    public void Update(UnityEngine.Ray ray) {
      // We might not clean up every doomed unit view here, thats fine, we'll get em next round.
      // We only delete until we find the first still-active one.
      while (doomedUnitViews.Count > 0) {
        var animationsEndTimeAndDoomedUnitView = doomedUnitViews[0];
        if (timer.GetTimeMs() >= animationsEndTimeAndDoomedUnitView.Key) {
          doomedUnitViews.RemoveAt(0);
          animationsEndTimeAndDoomedUnitView.Value.Destruct();
        } else {
          break;
        }
      }

      ConsumeEffects();

      timer.Update();
      cinematicTimer.Update();

      Location hoveredLocation = null;
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null) {
          hoveredLocation = LocationFor(hit.collider.gameObject);
        }
      }
      SetHighlightedLocation(hoveredLocation);

      playerController.Update();

      Unit unit = Unit.Null;
      TerrainTile tile = TerrainTile.Null;
      if (hoveredLocation != null) {
        unit = UnitAtLocation(hoveredLocation);
        tile = TileAtLocation(hoveredLocation);
      }
      playerController.LookAt(unit, tile);

      if (hoveredLocation != null && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
        if (inputSemaphore.locked) {
          Debug.LogError("Rejecting input, locked!");
        } else {
          playerController.OnTileMouseClick(hoveredLocation);
        }
      }
    }

    public void visitGameSetActingUnitEffect(GameSetActingUnitEffect effect) {}
    public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect effect) {}
    public void visitGameSetActionNumEffect(GameSetActionNumEffect effect) { }
    public void visitGameSetEvventEffect(GameSetEvventEffect effect) {}
    public void VisitIGameEvent(RevertedEventAsIGameEvent obj) {}
    public void VisitIGameEvent(SetGameSpeedEventAsIGameEvent obj) {
      timer.SetTimeSpeedMultiplier(obj.obj.percent / 100f);
    }

    public void VisitIGameEvent(WaitEventAsIGameEvent obj) {
      if (obj.obj.timeMs == 0) {

      } else {
        cinematicTimer.ScheduleTimer(obj.obj.timeMs, () => {
          throw new NotImplementedException();
          //serverSS.RequestTrigger(game.id, obj.obj.endTriggerName);
        });
      }
    }

    public void VisitIGameEvent(FlyCameraEventAsIGameEvent obj) {
      var cameraEndLookAtPosition = game.level.terrain.GetTileCenter(obj.obj.lookAt).ToUnity();
      cameraController.StartMovingCameraTo(cameraEndLookAtPosition, obj.obj.transitionTimeMs);
      cinematicTimer.ScheduleTimer(obj.obj.transitionTimeMs, () => {
        throw new NotImplementedException();
        //serverSS.RequestTrigger(game.id, obj.obj.endTriggerName);
      });
    }

    public void VisitIGameEvent(WaitForAnimationsEventAsIGameEvent obj) {
      throw new NotImplementedException();
    }

    public void OnCommMutListEffect(ICommMutListEffect effect) { effect.visitICommMutListEffect(this); }
    public void visitCommMutListCreateEffect(CommMutListCreateEffect effect) { }
    public void visitCommMutListDeleteEffect(CommMutListDeleteEffect effect) { }
    public void visitCommMutListRemoveEffect(CommMutListRemoveEffect effect) { }
    public void visitCommMutListAddEffect(CommMutListAddEffect effect) {
      var comm = game.root.GetComm(effect.element);

      bool isModal =
          comm.template is DramaticCommTemplateAsICommTemplate ||
          comm.template is NormalCommTemplateAsICommTemplate ||
          comm.template is DialogueCommTemplateAsICommTemplate;

      var buttons = new List<OverlayPresenter.PageButton>();
      for (int i = 0; i < comm.actions.Count; i++) {
        buttons.Add(
          new OverlayPresenter.PageButton(
            comm.actions[i].label,
            () => {
              if (isModal) {
                currentModalOverlay = null;
              }
              if (i < comm.actions.Count) {
                if (comm.actions[i].triggerName == "_exitGame") {
                  exitClicked?.Invoke();
                } else {
                  serverSS.RequestCommAction(game.id, comm.id, i);
                }
              }
            }));
      }

      var overlayPages = new List<OverlayPresenter.PageText>();
      foreach (var commText in comm.texts) {
        var textColor = new UnityEngine.Color(1, 1, 1);
        switch (commText.speakerRole) {
          case "kylin":
            textColor = new UnityEngine.Color(1, .2f, 0, 1);
            break;
          case "kylinBrother":
            textColor = new UnityEngine.Color(.5f, 1, 1, 1);
            break;
          case "narrator":
            textColor = new UnityEngine.Color(1, 1, 1, 1);
            break;
          default:
            Debug.LogWarning("Unknown role: " + commText.speakerRole);
            textColor = new UnityEngine.Color(1, 1, 1, 1);
            break;
        }
        overlayPages.Add(new OverlayPresenter.PageText(commText.text, textColor));
      }

      var pres =
        new OverlayPresenter(
          cinematicTimer,
          overlayPaneler,
          inputSemaphore,
          comm.template,
          overlayPages,
          buttons);
      if (isModal) {
        currentModalOverlay = pres;
      }
    }

    // Stalls every effect until theres no more current overlay.
    private class CommEffectStaller {
      GamePresenter gamePresenter;
      IEffectStaller stallEffect;
      IClock clock;
      IEffectObserver registeredObserver;
      EffectBroadcaster previewBroadcaster;

      public CommEffectStaller(IClock clock, GamePresenter gamePresenter, EffectBroadcaster previewBroadcaster, IEffectStaller stallEffect) {
        this.gamePresenter = gamePresenter;
        this.stallEffect = stallEffect;
        this.clock = clock;
        this.previewBroadcaster = previewBroadcaster;

        registeredObserver = OnEffect;
        previewBroadcaster.AddGlobalObserver(registeredObserver);
      }

      public void Destroy() {
        previewBroadcaster.RemoveGlobalObserver(registeredObserver);
      }

      public void OnEffect(IEffect effect) {
        if (gamePresenter.currentModalOverlay != null) {
          // "Check back next frame."
          stallEffect(clock.GetTimeMs() + 1);
        }
      }
    }
  }
}
