using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace IncendianFalls {
  public interface IGameMousedObserver {
    void OnUnitMouseClick(Unit unit);
    void OnUnitMouseIn(Unit unit);
    void OnUnitMouseOut(Unit unit);
    void OnTileMouseClick(Location location);
    void OnTileMouseIn(Location location);
    void OnTileMouseOut(Location location);
  }

  public class GamePresenter : IGameEffectVisitor, IGameEffectObserver, IUnitMutSetEffectVisitor, IUnitMutSetEffectObserver, IUnitMousedObserver, ITileMousedObserver {
    public List<IGameMousedObserver> observers = new List<IGameMousedObserver>();

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
        ITimer timer,
        SoundPlayer soundPlayer,
        ExecutionStaller resumeStaller,
        ExecutionStaller turnStaller,
        ISuperstructure ss,
        Game game,
        Instantiator instantiator,
        NarrationPanelView narrator) {
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
              timer, soundPlayer, resumeStaller, turnStaller, game, viewedLevel.terrain, unit, instantiator, narrator);
      unitPresenters[unit.id].observers.Add(this);
    }

    private void LoadLevel() {
      viewedLevel = game.level;

      this.terrainPresenter = new TerrainPresenter(viewedLevel.terrain, instantiator);
      terrainPresenter.observers.Add(this);

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

    public void OnUnitMouseClick(Unit unit) {
      foreach (var observer in observers) {
        observer.OnUnitMouseClick(unit);
      }
    }

    public void OnUnitMouseIn(Unit unit) {
      foreach (var observer in observers) {
        observer.OnUnitMouseIn(unit);
      }
    }

    public void OnUnitMouseOut(Unit unit) {
      foreach (var observer in observers) {
        observer.OnUnitMouseOut(unit);
      }
    }

    public void OnMouseClick(Location location) {
      foreach (var observer in observers) {
        observer.OnTileMouseClick(location);
      }
    }

    public void OnMouseIn(Location location) {
      foreach (var observer in observers) {
        observer.OnTileMouseIn(location);
      }
    }

    public void OnMouseOut(Location location) {
      foreach (var observer in observers) {
        observer.OnTileMouseOut(location);
      }
    }
  }
}
