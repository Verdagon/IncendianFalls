using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace AthPlayer {
  public delegate void OnLocationHovered(Location location);
  public delegate void OnLocationClicked(Location location);

  public class GamePresenter : IGameEffectVisitor, IGameEffectObserver, IUnitMutSetEffectVisitor, IUnitMutSetEffectObserver {
    public OnLocationHovered LocationHovered;
    public OnLocationClicked LocationClicked;

    IClock clock;
    ITimer timer;
    SoundPlayer soundPlayer;
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;
    ISuperstructure ss;
    Game game;
    Level viewedLevel;
    Instantiator instantiator;
    NarrationPanelView narrator;

    TerrainPresenter terrainPresenter;

    Dictionary<int, UnitPresenter> unitPresenters;

    public GamePresenter(
      IClock clock,
        ITimer timer,
        SoundPlayer soundPlayer,
        ExecutionStaller resumeStaller,
        ExecutionStaller turnStaller,
        ISuperstructure ss,
        Game game,
        Instantiator instantiator,
        NarrationPanelView narrator) {
      this.clock = clock;
      this.timer = timer;
      this.soundPlayer = soundPlayer;
      this.resumeStaller = resumeStaller;
      this.turnStaller = turnStaller;
      this.ss = ss;
      this.game = game;
      this.narrator = narrator;
      this.instantiator = instantiator;
      game.AddObserver(this);

      LoadLevel();
    }

    public TerrainPresenter GetTerrainPresenter() { return terrainPresenter; }
    public UnitPresenter GetPlayerPresenter() {
      return unitPresenters[game.player.id];
    }

    void RemoveUnit(int unitId) {
      this.unitPresenters.Remove(unitId);
    }

    void AddUnit(int unitId) {
      var unit = ss.GetRoot().GetUnit(unitId);
      unitPresenters[unitId] =
          new UnitPresenter(
              clock, timer, soundPlayer, resumeStaller, turnStaller, game, viewedLevel.terrain, unit, instantiator, narrator);
    }

    private void LoadLevel() {
      viewedLevel = game.level;

      this.terrainPresenter = new TerrainPresenter(clock, viewedLevel.terrain, instantiator);
      terrainPresenter.TerrainClicked += (location) => LocationClicked?.Invoke(location);
      terrainPresenter.TerrainHovered += (location) => LocationHovered?.Invoke(location);

      viewedLevel.units.AddObserver(this);
      this.unitPresenters = new Dictionary<int, UnitPresenter>();
      foreach (var unit in viewedLevel.units) {
        AddUnit(unit.id);
      }
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

    public void visitUnitMutSetCreateEffect(UnitMutSetCreateEffect effect) { }
    public void visitUnitMutSetDeleteEffect(UnitMutSetDeleteEffect effect) { }
    public void visitUnitMutSetAddEffect(UnitMutSetAddEffect effect) {
      AddUnit(effect.elementId);
    }
    public void visitUnitMutSetRemoveEffect(UnitMutSetRemoveEffect effect) {
      RemoveUnit(effect.id);
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
      return null;
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
  }
}
