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
    EffectBroadcaster broadcaster;
    Game game;
    Level viewedLevel;
    Instantiator instantiator;
    CameraController cameraController;

    InputSemaphore inputSemaphore;

    TerrainPresenter terrainPresenter;
    OverlayPresenterFactory overlayPresenterFactory;
    ShowError showError;
    ShowInstructions showInstructions;

    Dictionary<int, UnitPresenter> unitPresenters;

    PlayerController playerController;

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
      this.showError = showError;
      this.showInstructions = showInstructions;
      this.thinkingIndicator = thinkingIndicator;
      this.cameraController = cameraController;

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

      broadcaster = new EffectBroadcaster();

      var looker = new Looker(lookPanelView, broadcaster);

      timer = new SlowableTimerClock(1f);
      cinematicTimer = new SlowableTimerClock(1f);

      inputSemaphore.OnLocked += () => timer.SetTimeSpeedMultiplier(0f);
      inputSemaphore.OnUnlocked += () => timer.SetTimeSpeedMultiplier(1f);

      //resumeStaller = new ExecutionStaller(timer, timer);
      //turnStaller = new ExecutionStaller(timer, timer);

      //turnStaller.stalledEvent += (x) => {
      //  stalledIndicator.SetActive(true);
      //};
      //turnStaller.unstalledEvent += (x) => {
      //  stalledIndicator.SetActive(false);
      //};

      game.AddObserver(broadcaster, this);

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

      ConsumeEffects();
    }

    private void ConsumeEffects() {
      for (int i = 0; ; i++) {
        Asserts.Assert(i != 100);
        game.root.Transact(() => {
          // TODO, do proper waiting here.
          while (serverSS.waitingEffects.Count > 0) {
            var effect = serverSS.waitingEffects.Dequeue();
            new EffectApplier(game.root).Apply(effect);
            broadcaster.Route(effect);
          }
          return 0;
        });
        // At this point, game is caught up to server game.
        if (!game.WaitingOnPlayerInput()) {
          var error = serverSS.RequestResume(game.id);
          Asserts.Assert(error == "", error);
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
      this.unitPresenters[unitId].DestroyUnitPresenter();
      this.unitPresenters.Remove(unitId);
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
              timer, timer, soundPlayer, broadcaster, game, viewedLevel.terrain, unit, instantiator);
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
      foreach (var entry in unitPresenters) {
        entry.Value.DestroyUnitPresenter();
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
      Debug.LogError("unit mut set add! " + effect.element);
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
      throw new NotImplementedException();
      //overlayPresenterFactory(comm.template, comm.spea)
      // do nothing, itll kill itself.

      //var buttons = new List<OverlayPresenter.PageButton>();
      //for (int i = 0; i < comm.actions.Count; i++) {
      //  buttons.Add(
      //    new OverlayPresenter.PageButton(
      //      comm.actions[i].label,
      //      () => {
      //        if (comm.actions[i].triggerName == "_exitGame") {
      //          exitClicked?.Invoke();
      //        } else {
      //          ss.RequestTrigger(game.id, button.triggerName);
      //        }
      //      }));
      //}
      //return new OverlayPresenter(
      //  uiTimer,
      //  overlayPaneler,
      //  inputSemaphore,
      //  comm.template,
      //  comm.speakerRole,
      //  comm.isFirstInSequence,
      //  comm.isLastInSequence,
      //  comm.isObscuring,
      //  comm.text,
      //  buttons);
    }
  }
}
