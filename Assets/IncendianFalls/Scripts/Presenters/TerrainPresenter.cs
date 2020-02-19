using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace IncendianFalls {
  public delegate void OnTerrainClicked(Location location);
  public delegate void OnTerrainHovered(Location location);

  public class TerrainPresenter : ITerrainEffectVisitor, ITerrainEffectObserver, ITerrainTileByLocationMutMapEffectObserver, ITerrainTileByLocationMutMapEffectVisitor {
    public event OnTerrainClicked TerrainClicked;
    public event OnTerrainHovered TerrainHovered;

    Atharia.Model.Terrain terrain;
    Instantiator instantiator;
    Dictionary<Location, TerrainTilePresenter> tilePresenters = new Dictionary<Location, TerrainTilePresenter>();
    Location maybeHighlightLocation = null;

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


    public void SetHighlightLocation(Location location) {
      Location oldLocation = maybeHighlightLocation;
      if (location != maybeHighlightLocation) {
        Debug.LogError("Now over " + location);
        maybeHighlightLocation = location;
        if (oldLocation != null)
          UpdateLocationHighlighted(oldLocation);
        if (location != null)
          UpdateLocationHighlighted(location);
        TerrainHovered?.Invoke(location);
      }

      if (maybeHighlightLocation != null && Input.GetMouseButtonDown(0)) {
        if (tilePresenters.TryGetValue(maybeHighlightLocation, out var newMousedPhantomTilePresenter)) {
          TerrainClicked?.Invoke(maybeHighlightLocation);
        }
      }
    }

    private void UpdateLocationHighlighted(Location location) {
      if (tilePresenters.TryGetValue(location, out var newMousedTerrainTilePresenter)) {
        newMousedTerrainTilePresenter.SetHighlighted(location == maybeHighlightLocation);
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
