using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace AthPlayer {
  public delegate void OnTerrainClicked(Location location);
  public delegate void OnTerrainHovered(Location location);

  public class TerrainPresenter : ITerrainEffectVisitor, ITerrainEffectObserver, ITerrainTileByLocationMutMapEffectObserver, ITerrainTileByLocationMutMapEffectVisitor {
    IClock clock;
    ITimer timer;
    EffectBroadcaster preBroadcaster;
    EffectBroadcaster postBroadcaster;
    Atharia.Model.Terrain terrain;
    Instantiator instantiator;
    Dictionary<Location, TerrainTilePresenter> tilePresenters = new Dictionary<Location, TerrainTilePresenter>();

    SortedSet<Location> highlightLocations = new SortedSet<Location>();

    public TerrainPresenter(IClock clock, ITimer timer, EffectBroadcaster preBroadcaster, EffectBroadcaster postBroadcaster, Atharia.Model.Terrain terrain, Instantiator instantiator) {
      this.clock = clock;
      this.timer = timer;
      this.preBroadcaster = preBroadcaster;
      this.postBroadcaster = postBroadcaster;
      this.terrain = terrain;
      this.instantiator = instantiator;
      terrain.AddObserver(postBroadcaster, this);
      terrain.tiles.AddObserver(postBroadcaster, this);

      foreach (var locationAndTile in terrain.tiles) {
        addTerrainTile(locationAndTile.Key, locationAndTile.Value);
      }
    }

    public void addTerrainTile(Location location, TerrainTile tile) {
      var presenter = new TerrainTilePresenter(clock, timer, preBroadcaster, postBroadcaster, terrain, location, tile, instantiator);
      tilePresenters.Add(location, presenter);
    }

    public void DestroyTerrainPresenter() {
      foreach (var entry in tilePresenters) {
        entry.Value.DestroyTerrainTilePresenter();
      }
      if (terrain.Exists()) {
        if (terrain.tiles.Exists()) {
          terrain.tiles.RemoveObserver(postBroadcaster, this);
        }
        terrain.RemoveObserver(postBroadcaster, this);
      }
    }

    public void OnTerrainEffect(ITerrainEffect effect) { effect.visitITerrainEffect(this); }
    public void visitTerrainCreateEffect(TerrainCreateEffect effect) { }
    public void visitTerrainDeleteEffect(TerrainDeleteEffect effect) { }
    public void visitTerrainSetPatternEffect(TerrainSetPatternEffect effect) { }

    public void OnTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) { effect.visitITerrainTileByLocationMutMapEffect(this); }
    public void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect) {
      addTerrainTile(effect.key, terrain.tiles[effect.key]);
    }
    public void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect) { }
    public void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect) { }
    public void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect) { }


    public void SetHighlightLocations(SortedSet<Location> newLocations) {
      SortedSet<Location> oldLocations = highlightLocations;
      highlightLocations = newLocations;

      SortedSet<Location> locationsToUpdate = new SortedSet<Location>();
      foreach (var oldLocation in oldLocations) {
        if (!newLocations.Contains(oldLocation)) {
          locationsToUpdate.Add(oldLocation);
        }
      }
      foreach (var newLocation in newLocations) {
        if (!oldLocations.Contains(newLocation)) {
          locationsToUpdate.Add(newLocation);
        }
      }
      foreach (var locationToUpdate in locationsToUpdate) {
        UpdateLocationHighlighted(locationToUpdate);
      }
    }

    private void UpdateLocationHighlighted(Location location) {
      if (tilePresenters.TryGetValue(location, out var newMousedTerrainTilePresenter)) {
        newMousedTerrainTilePresenter.SetHighlighted(highlightLocations.Contains(location));
      }
    }

    public Location LocationFor(GameObject gameObject) {
      var mousedTerrainTilePresenterTile = gameObject.GetComponentInParent<TerrainTilePresenterTile>();
      if (mousedTerrainTilePresenterTile) {
        return mousedTerrainTilePresenterTile.presenter.location;
      }
      return null;
    }
  }
}
