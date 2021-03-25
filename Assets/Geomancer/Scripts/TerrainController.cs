using System;
using System.Collections.Generic;
using Geomancer.Model;
using UnityEngine;
using Domino;

namespace Geomancer {
  public delegate void OnTerrainTileHovered(Location location);
  public delegate void OnTerrainTileClicked(Location location);
  public delegate void OnPhantomTileClicked(Location location);

  public class TerrainPresenter {
    public OnTerrainTileHovered TerrainTileHovered;
    public OnTerrainTileClicked TerrainTileClicked;
    public OnPhantomTileClicked PhantomTileClicked;

    IClock clock;
    ITimer timer;
    MemberToViewMapper vivimap;
    public readonly Geomancer.Model.Terrain terrain;
    Instantiator instantiator;
    Dictionary<Location, TerrainTilePresenter> tilePresenters = new Dictionary<Location, TerrainTilePresenter>();
    Dictionary<Location, PhantomTilePresenter> phantomTilePresenters = new Dictionary<Location, PhantomTilePresenter>();

    Location maybeMouseHighlightedLocation = null;
    private SortedSet<Location> highlightedLocations = new SortedSet<Location>();

    public TerrainPresenter(IClock clock,
      ITimer timer, MemberToViewMapper vivimap, Geomancer.Model.Terrain terrain, Instantiator instantiator) {
      this.clock = clock;
      this.timer = timer;
      this.vivimap = vivimap;
      this.terrain = terrain;
      this.instantiator = instantiator;

      foreach (var locationAndTile in terrain.tiles) {
        addTerrainTile(locationAndTile.Key, locationAndTile.Value);
      }

      RefreshPhantomTiles();
    }

    public void AddTile(TerrainTilePresenter presenter) {
      tilePresenters.Add(presenter.location, presenter);
    }

    public Location GetMaybeMouseHighlightLocation() { return maybeMouseHighlightedLocation; }

    public void DestroyTerrainPresenter() {
      foreach (var entry in tilePresenters) {
        entry.Value.DestroyTerrainTilePresenter();
      }
    }

    public void UpdateMouse(UnityEngine.Ray ray) {
      Location oldLocation = maybeMouseHighlightedLocation;
      var location = LocationUnderMouse(ray);
      if (location != maybeMouseHighlightedLocation) {
        maybeMouseHighlightedLocation = location;
        UpdateLocationHighlighted(oldLocation);
        UpdateLocationHighlighted(location);
        TerrainTileHovered?.Invoke(maybeMouseHighlightedLocation);
      }

      if (Input.GetMouseButtonDown(0)) {
        if (maybeMouseHighlightedLocation != null) {
          if (tilePresenters.TryGetValue(maybeMouseHighlightedLocation, out var newMousedTerrainTilePresenter)) {
            TerrainTileClicked?.Invoke(maybeMouseHighlightedLocation);
          }
          if (phantomTilePresenters.TryGetValue(maybeMouseHighlightedLocation, out var newMousedPhantomTilePresenter)) {
            PhantomTileClicked?.Invoke(maybeMouseHighlightedLocation);
          }
        }
      }
    }

    private void UpdateLocationHighlighted(Location location) {
      bool highlighted = location == maybeMouseHighlightedLocation;
      bool selected = highlightedLocations.Contains(location);
      if (location != null) {
        if (tilePresenters.TryGetValue(location, out var newMousedTerrainTilePresenter)) {
          newMousedTerrainTilePresenter.SetHighlighted(highlighted);
          newMousedTerrainTilePresenter.SetSelected(selected);
        }
        if (phantomTilePresenters.TryGetValue(location, out var newMousedPhantomTilePresenter)) {
          // Cant select a phantom tile
          newMousedPhantomTilePresenter.SetHighlighted(highlighted);
        }
      }
    }

    private Location LocationUnderMouse(UnityEngine.Ray ray) {
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null) {
          var gameObject = hit.collider.gameObject;
          var mousedPhantomTilePresenterTile = gameObject.GetComponentInParent<PhantomTilePresenterTile>();
          if (mousedPhantomTilePresenterTile) {
            return mousedPhantomTilePresenterTile.presenter.location;
          }
          var mousedTerrainTilePresenterTile = gameObject.GetComponentInParent<TerrainTilePresenterTile>();
          if (mousedTerrainTilePresenterTile) {
            return mousedTerrainTilePresenterTile.presenter.location;
          }
        }
      }
      return null;
    }
    
    public void AddTile(TerrainTile tile) {
      if (phantomTilePresenters.TryGetValue(tile.location, out var presenter)) {
        presenter.DestroyPhantomTilePresenter();
        phantomTilePresenters.Remove(tile.location);
      }
      terrain.tiles.Add(tile.location, tile);
      addTerrainTile(tile.location, terrain.tiles[tile.location]);
      RefreshPhantomTiles();
    }
    public void RemoveTile(TerrainTile tile) {
      tilePresenters.Remove(tile.location);
      var newHighlightedLocations = new SortedSet<Location>(highlightedLocations);
      newHighlightedLocations.Remove(tile.location);
      SetHighlightedLocations(newHighlightedLocations);
      RefreshPhantomTiles();
    }
    public TerrainTilePresenter GetTilePresenter(Location location) {
      if (tilePresenters.TryGetValue(location, out var presenter)) {
        return presenter;
      }
      return null;
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
      var presenter = new TerrainTilePresenter(clock, timer, vivimap, terrain, location, tile, instantiator);
      tilePresenters.Add(location, presenter);
    }

    private void addPhantomTile(Location location) {
      var presenter = new PhantomTilePresenter(clock, timer, terrain.pattern, location, instantiator);
      phantomTilePresenters.Add(location, presenter);
    }

    public void SetHighlightedLocations(SortedSet<Location> locations) {
      var (addedLocations, removedLocations) = Geomancer.Model.SetUtils.Diff(highlightedLocations, locations);
      highlightedLocations = locations;
      foreach (var addedLocation in addedLocations) {
        UpdateLocationHighlighted(addedLocation);
      }
      foreach (var removedLocation in removedLocations) {
        UpdateLocationHighlighted(removedLocation);
      }
    }
  }
}
