﻿using System;
using System.Collections.Generic;
using IncendianFalls;
using Geomancer.Model;
using UnityEngine;
using Domino;

namespace Geomancer {
  public class TerrainTilePresenterTile : MonoBehaviour {
    // PhantomTilePresenter attaches this to the TileView it creates, so that when EditorPresenter
    // raycasts, it can know the PhantomTilePresenter that owns this TileView.
    // This approach is an implementation detail of the Editor, and shouldnt enter Domino.
    public TerrainTilePresenter presenter;

    public void Init(TerrainTilePresenter presenter) {
      this.presenter = presenter;
    }
  }

  public class TerrainTilePresenter : IStrMutListEffectObserver, IStrMutListEffectVisitor {
    public delegate void OnMouseInEvent();
    public delegate void OnMouseOutEvent();
    public delegate void OnMouseClickEvent();

    Vivimap vivimap;
    Geomancer.Model.Terrain terrain;
    public readonly Location location;
    TerrainTile terrainTile;
    Instantiator instantiator;

    Vector3 tileCenter;
    TileView tileView;
    UnitView unitView;

    //public event OnMouseInEvent mouseIn;
    //public event OnMouseOutEvent mouseOut;
    //public event OnMouseClickEvent mouseClick;

    public TerrainTilePresenter(
        Vivimap vivimap,
        Geomancer.Model.Terrain terrain,
        Location location,
        TerrainTile terrainTile,
        Instantiator instantiator) {
      this.vivimap = vivimap;
      this.location = location;
      this.terrain = terrain;
      this.terrainTile = terrainTile;
      this.instantiator = instantiator;

      terrainTile.members.AddObserver(this);

      var positionVec2 = terrain.pattern.GetTileCenter(location);

      tileCenter =
        new UnityEngine.Vector3(
                  positionVec2.x,
                  terrainTile.elevation * terrain.elevationStepHeight,
                  positionVec2.y);

      ResetViews();
    }
    
    public void SetTinted(bool highlighted, bool selected) {
      var (tileDescription, maybeUnitDescription) = GetDescriptions(highlighted, selected);
      tileView.SetDescription(tileDescription);
    }

    private void ResetViews() {
      var (tileDescription, maybeUnitDescription) = GetDescriptions(false, false);

      if (tileView != null) {
        tileView.DestroyTile();
        tileView = null;
      }

      tileView = instantiator.CreateTileView(tileCenter, tileDescription);
      tileView.gameObject.AddComponent<TerrainTilePresenterTile>().Init(this);
      tileView.SetDescription(tileDescription);

      if (unitView) {
        unitView.DestroyUnit();
        unitView = null;
      }

      if (maybeUnitDescription != null) {
        unitView = instantiator.CreateUnitView(null, tileCenter, maybeUnitDescription);
        unitView.SetDescription(maybeUnitDescription);
      }
    }

    private (TileDescription, UnitDescription) GetDescriptions(bool highlighted, bool selected) {
      int lowestNeighborElevation = int.MaxValue;
      foreach (var adjacentLocation in terrain.GetAdjacentExistingLocations(location, false)) {
        var adjacentTerrainTile = terrain.tiles[adjacentLocation];
        lowestNeighborElevation = Math.Min(lowestNeighborElevation, adjacentTerrainTile.elevation);
      }
      int neededDepth = Math.Max(1, terrainTile.elevation - lowestNeighborElevation);

      var patternTile = terrain.pattern.patternTiles[location.indexInGroup];

      var defaultTileDescription =
          new TileDescription(
              terrain.elevationStepHeight,
              patternTile.rotateDegrees,
              neededDepth,
              new ExtrudedSymbolDescription(
                RenderPriority.TILE,
                new SymbolDescription(
                    ((char)('0' + patternTile.shapeIndex)).ToString(),
                    new Color(1, 0, 1),
                    patternTile.rotateDegrees,
                    OutlineMode.WithOutline,
                    new Color(0, 1.5f, 1.5f)),
                true,
                new Color(1, .5f, 0)),
              null,
              null,
              new SortedDictionary<int, ExtrudedSymbolDescription>());

      var defaultUnitDescription =
        new UnitDescription(
          null,
          new DominoDescription(false, new Color(.5f, 0, .5f)),
          new ExtrudedSymbolDescription(
            RenderPriority.DOMINO,
            new SymbolDescription(
              "a", new Color(0, 1, 0), 45, OutlineMode.WithBackOutline, new Color(0, 0, 0)),
            true,
            new Color(0, 0, 0)),
          new List<KeyValuePair<int, ExtrudedSymbolDescription>>(),
          1,
          1);

      var members = new List<String>();
      foreach (var member in this.terrainTile.members) {
        members.Add(member);
      }
      var (tileDescription, unitDescription) =
        vivimap.Vivify(defaultTileDescription, defaultUnitDescription, members);
      if (highlighted || selected) {
        Color frontColor;
        if (selected && highlighted) {
          frontColor = (tileDescription.tileSymbolDescription.symbol.frontColor * 5 + 3 * new Color(1, 1, 1, 1)) / 8;
        } else if (selected) {
          frontColor = (tileDescription.tileSymbolDescription.symbol.frontColor * 6 + 2 * new Color(1, 1, 1, 1)) / 8;
        } else if (highlighted) {
          frontColor = (tileDescription.tileSymbolDescription.symbol.frontColor * 7 + 1 * new Color(1, 1, 1, 1)) / 8;
        } else {
          frontColor = tileDescription.tileSymbolDescription.symbol.frontColor;
        }
        tileDescription =
          new TileDescription(
            tileDescription.elevationStepHeight,
            tileDescription.tileRotationDegrees,
            tileDescription.depth,
            new ExtrudedSymbolDescription(
              tileDescription.tileSymbolDescription.renderPriority,
              new SymbolDescription(
                tileDescription.tileSymbolDescription.symbol.symbolId,
                frontColor,
                tileDescription.tileSymbolDescription.symbol.rotationDegrees,
                tileDescription.tileSymbolDescription.symbol.withOutline,
                tileDescription.tileSymbolDescription.symbol.outlineColor),
              tileDescription.tileSymbolDescription.extruded,
              tileDescription.tileSymbolDescription.sidesColor),
            tileDescription.maybeOverlaySymbolDescription,
            tileDescription.maybeFeatureSymbolDescription,
            tileDescription.itemSymbolDescriptionByItemId);
      }
      return (tileDescription, unitDescription);
    }

    public void DestroyTerrainTilePresenter() {
      tileView.DestroyTile();
    }

    //public void OnMouseClick() {
    //  mouseClick.Invoke();
    //}

    //public void OnMouseEnter() {
    //  mouseIn.Invoke();
    //}

    //public void OnMouseExit() {
    //  mouseOut.Invoke();
    //}

    public void OnStrMutListEffect(IStrMutListEffect effect) {
      effect.visit(this);
    }

    public void visitStrMutListCreateEffect(StrMutListCreateEffect effect) {}

    public void visitStrMutListDeleteEffect(StrMutListDeleteEffect effect) {}

    public void visitStrMutListAddEffect(StrMutListAddEffect effect) {
      ResetViews();
    }

    public void visitStrMutListRemoveEffect(StrMutListRemoveEffect effect) {
      ResetViews();
    }
  }
}