using System;
using System.Collections.Generic;
using Geomancer.Model;
using UnityEngine;
using Domino;

namespace Geomancer {
  public delegate void OnTerrainTileClicked(Location location);
  public delegate void OnPhantomTileClicked(Location location);

  public class TerrainPresenter : ITerrainEffectVisitor, ITerrainEffectObserver, ITerrainTileByLocationMutMapEffectObserver, ITerrainTileByLocationMutMapEffectVisitor {
    public OnTerrainTileClicked TerrainTileClicked;
    public OnPhantomTileClicked PhantomTileClicked;

    Vivimap vivimap;
    Geomancer.Model.Terrain terrain;
    Instantiator instantiator;
    Dictionary<Location, TerrainTilePresenter> tilePresenters = new Dictionary<Location, TerrainTilePresenter>();
    Dictionary<Location, PhantomTilePresenter> phantomTilePresenters = new Dictionary<Location, PhantomTilePresenter>();

    bool isMouseHighlighting = false;
    Location mouseHighlightedLocation;

    public TerrainPresenter(Vivimap vivimap, Geomancer.Model.Terrain terrain, Instantiator instantiator) {
      this.vivimap = vivimap;
      this.terrain = terrain;
      this.instantiator = instantiator;
      terrain.AddObserver(this);
      terrain.tiles.AddObserver(this);

      foreach (var locationAndTile in terrain.tiles) {
        addTerrainTile(locationAndTile.Key, locationAndTile.Value);
      }

      RefreshPhantomTiles();
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

    public void UpdateMouse(UnityEngine.Ray ray) {
      bool newMouseIsHighlighting = LocationUnderMouse(ray, out var location);
      if (newMouseIsHighlighting != isMouseHighlighting || location != mouseHighlightedLocation) {
        if (isMouseHighlighting) {
          if (tilePresenters.TryGetValue(mouseHighlightedLocation, out var oldMousedTerrainTilePresenter)) {
            oldMousedTerrainTilePresenter.SetHighlighted(false);
          }
          if (phantomTilePresenters.TryGetValue(mouseHighlightedLocation, out var oldMousedPhantomTilePresenter)) {
            oldMousedPhantomTilePresenter.SetHighlighted(false);
          }
        }
        isMouseHighlighting = newMouseIsHighlighting;
        mouseHighlightedLocation = location;
        if (isMouseHighlighting) {
          if (tilePresenters.TryGetValue(mouseHighlightedLocation, out var newMousedTerrainTilePresenter)) {
            newMousedTerrainTilePresenter.SetHighlighted(true);
          }
          if (phantomTilePresenters.TryGetValue(mouseHighlightedLocation, out var newMousedPhantomTilePresenter)) {
            newMousedPhantomTilePresenter.SetHighlighted(true);
          }
        }
      }

      if (Input.GetMouseButtonDown(0)) {
        if (tilePresenters.TryGetValue(mouseHighlightedLocation, out var newMousedTerrainTilePresenter)) {
          TerrainTileClicked?.Invoke(mouseHighlightedLocation);
        }
        if (phantomTilePresenters.TryGetValue(mouseHighlightedLocation, out var newMousedPhantomTilePresenter)) {
          PhantomTileClicked?.Invoke(mouseHighlightedLocation);
        }
      }
    }

    private bool LocationUnderMouse(UnityEngine.Ray ray, out Location location) {
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null) {
          var gameObject = hit.collider.gameObject;
          var mousedPhantomTilePresenterTile = gameObject.GetComponentInParent<PhantomTilePresenterTile>();
          if (mousedPhantomTilePresenterTile) {
            location = mousedPhantomTilePresenterTile.presenter.location;
            return true;
          }
          var mousedTerrainTilePresenterTile = gameObject.GetComponentInParent<TerrainTilePresenterTile>();
          if (mousedTerrainTilePresenterTile) {
            location = mousedTerrainTilePresenterTile.presenter.location;
            return true;
          }
        }
      }
      location = new Location(0, 0, 0);
      return false;
    }

    public void OnTerrainEffect(ITerrainEffect effect) { effect.visit(this); }
    public void visitTerrainCreateEffect(TerrainCreateEffect effect) { }
    public void visitTerrainDeleteEffect(TerrainDeleteEffect effect) { }

    public void OnTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) { effect.visit(this); }
    public void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect) {
      if (phantomTilePresenters.TryGetValue(effect.key, out var presenter)) {
        presenter.DestroyPhantomTilePresenter();
        phantomTilePresenters.Remove(effect.key);
      }
      addTerrainTile(effect.key, terrain.tiles[effect.key]);
      RefreshPhantomTiles();
    }
    public void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect) { }
    public void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect) { }
    public void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect) {

    }

    private void RefreshPhantomTiles() {
      var phantomTileLocations =
        terrain.pattern.GetAdjacentLocations(new SortedSet<Location>(terrain.tiles.Keys), false, true);
      var previousPhantomTileLocations = phantomTilePresenters.Keys;

      var addedPhantomTileLocations = new SortedSet<Location>(phantomTileLocations);
      SetUtils.RemoveAll(addedPhantomTileLocations, previousPhantomTileLocations);

      var removedPhantomTileLocations = new SortedSet<Location>(previousPhantomTileLocations);
      SetUtils.RemoveAll(removedPhantomTileLocations, phantomTileLocations);

      foreach (var removedPhantomTileLocation in removedPhantomTileLocations) {
        removePhantomTile(removedPhantomTileLocation);
      }

      foreach (var addedPhantomTileLocation in addedPhantomTileLocations) {
        addPhantomTile(addedPhantomTileLocation);
      }
    }

    private void removePhantomTile(Location removedPhantomTileLocation) {
      phantomTilePresenters[removedPhantomTileLocation].DestroyPhantomTilePresenter();
      phantomTilePresenters.Remove(removedPhantomTileLocation);
    }

    private void addTerrainTile(Location location, TerrainTile tile) {
      var presenter = new TerrainTilePresenter(vivimap, terrain, location, tile, instantiator);
      tilePresenters.Add(location, presenter);
      //presenter.mouseClick += () => OnMouseClick(location);
      //presenter.mouseIn += () => OnMouseIn(location);
      //presenter.mouseOut += () => OnMouseOut(location);
      //  TileClicked += (loc) => {
      //  Debug.Log("tile clicked in terrain pres!");
      //  TerrainClicked?.Invoke(loc);
      //};
    }

    private void addPhantomTile(Location location) {
      var presenter = new PhantomTilePresenter(terrain.pattern, location, instantiator);
      phantomTilePresenters.Add(location, presenter);
    }
  }
}
