using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace AthPlayer {
  public delegate void OnLocationHovered(Location location);
  public delegate void OnLocationClicked(Location location);

  public class GamePresenter : IGameEffectVisitor, IGameEffectObserver, IUnitMutSetEffectVisitor, IUnitMutSetEffectObserver, IGameEventVisitor, IIGameEventMutListEffectVisitor, IIGameEventMutListEffectObserver {
    public OnLocationHovered LocationHovered;
    public OnLocationClicked LocationClicked;

    SlowableTimerClock timer;
    SlowableTimerClock cinematicTimer;
    SoundPlayer soundPlayer;

    // resumeStaller is for GamePresenter to pay attention to, so it can tell the game
    // to resume the other units (and player's pre-acting and post-acting things).
    ExecutionStaller resumeStaller;
    // turnStaller is for the PlayerController to pay attention to, so it can delay the
    // player's input.
    ExecutionStaller turnStaller;
    // If we delay the resumeStaller but not the turnStaller, that means we just did an
    // animation where we're okay if the player interrupts it. Don't know if any of
    // these cases exist.
    // If we delay the turnStaller but not the resumeStaller, that means we just did an
    // animation where we're okay if we do more animations and stuff, we just dont want
    // the player to act yet. Most basic case is a unit moving.

    GameObject thinkingIndicator;
    ISuperstructure ss;
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
        ISuperstructure ss,
        InputSemaphore inputSemaphore,
        Game game,
        Instantiator instantiator,
        OverlayPresenterFactory overlayPresenterFactory,
        ShowError showError,
        ShowInstructions showInstructions,
        CameraController cameraController,
        GameObject stalledIndicator,
        Looker looker,
        OverlayPaneler overlayPaneler) {
      this.soundPlayer = soundPlayer;
      this.ss = ss;
      this.game = game;
      this.overlayPresenterFactory = overlayPresenterFactory;
      this.inputSemaphore = inputSemaphore;
      this.instantiator = instantiator;
      this.showError = showError;
      this.showInstructions = showInstructions;
      this.thinkingIndicator = thinkingIndicator;
      this.cameraController = cameraController;


      timer = new SlowableTimerClock(1f);
      cinematicTimer = new SlowableTimerClock(1f);

      inputSemaphore.OnLocked += () => timer.SetTimeSpeedMultiplier(0f);
      inputSemaphore.OnUnlocked += () => timer.SetTimeSpeedMultiplier(1f);

      resumeStaller = new ExecutionStaller(timer, timer);
      turnStaller = new ExecutionStaller(timer, timer);

      turnStaller.stalledEvent += (x) => {
        stalledIndicator.SetActive(true);
      };
      turnStaller.unstalledEvent += (x) => {
        stalledIndicator.SetActive(false);
      };

      game.AddObserver(this);
      game.events.AddObserver(this);

      foreach (var e in game.events) {
        e.Visit(this);
      }

      playerController =
          new PlayerController(
              timer,
              cinematicTimer,
              resumeStaller,
              turnStaller,
              inputSemaphore,
              ss,
              ss.GetSuperstate(game.id),
              game,
              looker,
              overlayPaneler,
              overlayPresenterFactory,
              cameraController,
              showError,
              thinkingIndicator);

      LoadLevel();
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
              timer, timer, soundPlayer, resumeStaller, turnStaller, game, viewedLevel.terrain, unit, instantiator);
    }

    private void LoadLevel() {
      viewedLevel = game.level;

      this.terrainPresenter = new TerrainPresenter(timer, timer, viewedLevel.terrain, instantiator);
      terrainPresenter.TerrainClicked += (location) => LocationClicked?.Invoke(location);
      terrainPresenter.TerrainHovered += (location) => LocationHovered?.Invoke(location);

      viewedLevel.units.AddObserver(this);
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
        viewedLevel.units.RemoveObserver(this);
      }

      terrainPresenter.DestroyTerrainPresenter();
      terrainPresenter = null;

      viewedLevel = null;
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this); }
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
      AddUnit(effect.elementId);
    }
    public void visitUnitMutSetRemoveEffect(UnitMutSetRemoveEffect effect) {
      RemoveUnit(effect.elementId);
    }
    public void OnUnitMutSetEffect(IUnitMutSetEffect effect) { effect.visit(this); }

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


    public void OnIGameEventMutListEffect(IIGameEventMutListEffect effect) { effect.visit(this); }
    public void visitIGameEventMutListCreateEffect(IGameEventMutListCreateEffect effect) { }
    public void visitIGameEventMutListDeleteEffect(IGameEventMutListDeleteEffect effect) { }
    public void visitIGameEventMutListRemoveEffect(IGameEventMutListRemoveEffect effect) { }
    public void visitIGameEventMutListAddEffect(IGameEventMutListAddEffect effect) { effect.element.Visit(this); }
    public void Visit(FlyCameraEventAsIGameEvent obj) {
      var cameraEndLookAtPosition = game.level.terrain.GetTileCenter(obj.obj.lookAt).ToUnity();
      cameraController.StartMovingCameraTo(cameraEndLookAtPosition, obj.obj.transitionTimeMs);
      Debug.Log("Moving camera!");
      cinematicTimer.ScheduleTimer(obj.obj.transitionTimeMs, () => ss.RequestTrigger(game.id, obj.obj.endTriggerName));
    }
    public void Visit(ShowOverlayEventAsIGameEvent obj) {
      var overlayPresenter = overlayPresenterFactory(obj.obj);
      // do nothing, itll kill itself.
    }

    public void Visit(SetGameSpeedEventAsIGameEvent obj) {
      timer.SetTimeSpeedMultiplier(obj.obj.percent / 100f);
    }

    public void Visit(WaitEventAsIGameEvent obj) {
      if (obj.obj.timeMs == 0) {

      } else {
        cinematicTimer.ScheduleTimer(obj.obj.timeMs, () => ss.RequestTrigger(game.id, obj.obj.endTriggerName));
      }
    }

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
  }
}
