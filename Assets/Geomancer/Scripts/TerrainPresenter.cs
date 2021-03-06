﻿using System;
using System.Collections.Generic;
using Geomancer.Model;
using UnityEngine;
using Domino;

namespace Geomancer {
  public delegate void OnTerrainTileHovered(Location location);
  public delegate void OnTerrainTileClicked(Location location);
  public delegate void OnPhantomTileClicked(Location location);

  public class TerrainPresenter : ITerrainEffectVisitor, ITerrainEffectObserver, ITerrainTileByLocationMutMapEffectObserver, ITerrainTileByLocationMutMapEffectVisitor {
    public OnTerrainTileHovered TerrainTileHovered;
    public OnTerrainTileClicked TerrainTileClicked;
    public OnPhantomTileClicked PhantomTileClicked;

    IClock clock;
    ITimer timer;
    EffectBroadcaster broadcaster;
  MemberToViewMapper vivimap;
    Geomancer.Model.Terrain terrain;
    Instantiator instantiator;
    Dictionary<Location, TerrainTilePresenter> tilePresenters = new Dictionary<Location, TerrainTilePresenter>();
    Dictionary<Location, PhantomTilePresenter> phantomTilePresenters = new Dictionary<Location, PhantomTilePresenter>();

    Location maybeMouseHighlightedLocation = null;
    private SortedSet<Location> highlightedLocations = new SortedSet<Location>();

    public TerrainPresenter(IClock clock,
      ITimer timer, EffectBroadcaster broadcaster, MemberToViewMapper vivimap, Geomancer.Model.Terrain terrain, Instantiator instantiator) {
      this.clock = clock;
      this.timer = timer;
      this.broadcaster = broadcaster;
      this.vivimap = vivimap;
      this.terrain = terrain;
      this.instantiator = instantiator;
      terrain.AddObserver(broadcaster, this);
      terrain.tiles.AddObserver(broadcaster, this);

      foreach (var locationAndTile in terrain.tiles) {
        addTerrainTile(locationAndTile.Key, locationAndTile.Value);
      }

      RefreshPhantomTiles();
    }

    public Location GetMaybeMouseHighlightLocation() { return maybeMouseHighlightedLocation; }

    public void DestroyTerrainPresenter() {
      foreach (var entry in tilePresenters) {
        entry.Value.DestroyTerrainTilePresenter();
      }
      if (terrain.Exists()) {
        if (terrain.tiles.Exists()) {
          terrain.tiles.RemoveObserver(broadcaster, this);
        }
        terrain.RemoveObserver(broadcaster, this);
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
          newMousedTerrainTilePresenter.SetTinted(highlighted, selected);
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

    public void OnTerrainEffect(ITerrainEffect effect) { effect.visitITerrainEffect(this); }
    public void visitTerrainCreateEffect(TerrainCreateEffect effect) { }
    public void visitTerrainDeleteEffect(TerrainDeleteEffect effect) { }

    public void OnTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect) { effect.visitITerrainTileByLocationMutMapEffect(this); }
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
      tilePresenters.Remove(effect.key);
      var newHighlightedLocations = new SortedSet<Location>(highlightedLocations);
      newHighlightedLocations.Remove(effect.key);
      SetHighlightedLocations(newHighlightedLocations);
      RefreshPhantomTiles();
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
      var presenter = new TerrainTilePresenter(clock, timer, broadcaster, vivimap, terrain, location, tile, instantiator);
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
