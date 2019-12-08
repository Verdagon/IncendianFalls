using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace IncendianFalls {
  public delegate void OnTerrainClicked(Location location);

  public class TerrainPresenter : ITerrainEffectVisitor, ITerrainEffectObserver, ITerrainTileByLocationMutMapEffectObserver, ITerrainTileByLocationMutMapEffectVisitor, ITileMousedObserver {
    public List<ITileMousedObserver> observers = new List<ITileMousedObserver>();

    Atharia.Model.Terrain terrain;
    Instantiator instantiator;
    Dictionary<Location, TerrainTilePresenter> tilePresenters = new Dictionary<Location, TerrainTilePresenter>();
    
    public TerrainPresenter(Atharia.Model.Terrain terrain, Instantiator instantiator) {
      this.terrain = terrain;
      this.instantiator = instantiator;
      terrain.AddObserver(this);
      terrain.tiles.AddObserver(this);

      foreach (var locationAndTile in terrain.tiles) {
        addTerrainTile(locationAndTile.Key, locationAndTile.Value);
      }
    }

    public void addTerrainTile(Location location, TerrainTile tile) {
      var presenter = new TerrainTilePresenter(terrain, location, tile, instantiator);
      tilePresenters.Add(location, presenter);
      presenter.observers.Add(this);
      //  TileClicked += (loc) => {
      //  Debug.Log("tile clicked in terrain pres!");
      //  TerrainClicked?.Invoke(loc);
      //};
    }

    public void DestroyTerrainPresenter() {
      foreach (var entry in tilePresenters) {
        entry.Value.DestroyTerrainTilePresenter();
      }
      if (terrain.Exists()) {
        if (terrain.tiles.Exists()) {
          terrain.tiles.RemoveObserver(this);
        }
        terrain.RemoveObserver(this);
      }
    }

    public void OnTerrainEffect(ITerrainEffect effect) { effect.visit(this); }
    public void visitTerrainCreateEffect(TerrainCreateEffect effect) { }
    public void visitTerrainDeleteEffect(TerrainDeleteEffect effect) { }
    public void visitTerrainSetPatternEffect(TerrainSetPatternEffect effect) { }

    public void OnTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) { effect.visit(this); }
    public void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect) {
      addTerrainTile(effect.key, terrain.tiles[effect.key]);
    }
    public void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect) { }
    public void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect) { }
    public void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect) { }

    public void OnMouseClick(Location location) {
      foreach (var observer in observers) {
        observer.OnMouseClick(location);
      }
    }

    public void OnMouseIn(Location location) {
      foreach (var observer in observers) {
        observer.OnMouseIn(location);
      }
    }

    public void OnMouseOut(Location location) {
      foreach (var observer in observers) {
        observer.OnMouseOut(location);
      }
    }
  }
}
