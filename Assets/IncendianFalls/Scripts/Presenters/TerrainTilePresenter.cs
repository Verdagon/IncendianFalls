﻿using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace IncendianFalls {
  public interface ITileMousedObserver {
    void OnMouseClick(Location location);
    void OnMouseIn(Location location);
    void OnMouseOut(Location location);
  }

  public class TerrainTilePresenter : IITerrainTileComponentMutBunchObserver {
    public List<ITileMousedObserver> observers = new List<ITileMousedObserver>();

    Atharia.Model.Terrain terrain;
    Location location;
    TerrainTile terrainTile;
    Instantiator instantiator;

    TileView tileView;

    ITerrainTileComponentMutBunchBroadcaster componentsBroadcaster;

    public TerrainTilePresenter(
        Atharia.Model.Terrain terrain,
        Location location,
        TerrainTile terrainTile,
        Instantiator instantiator) {
      this.location = location;
      this.terrain = terrain;
      this.terrainTile = terrainTile;
      this.instantiator = instantiator;

      var positionVec2 = terrain.pattern.GetTileCenter(location);
      tileView =
          instantiator.CreateTileView(
              new UnityEngine.Vector3(
                  positionVec2.x,
                  terrainTile.elevation * terrain.elevationStepHeight,
                  positionVec2.y),
              GetDescription());

      componentsBroadcaster = new ITerrainTileComponentMutBunchBroadcaster(terrainTile.components);
      componentsBroadcaster.AddObserver(this);

      tileView.SetDescription(GetDescription());
    }

    private TileDescription GetDescription() {
      int lowestNeighborElevation = int.MaxValue;
      foreach (var adjacentLocation in terrain.GetAdjacentExistingLocations(location, false)) {
        var adjacentTerrainTile = terrain.tiles[adjacentLocation];
        lowestNeighborElevation = Math.Min(lowestNeighborElevation, adjacentTerrainTile.elevation);
      }
      int neededDepth = Math.Max(1, terrainTile.elevation - lowestNeighborElevation);

      DetermineTileAppearance(
          terrainTile,
          out Color topColor,
          out Color outlineColor,
          out Color sideColor,
          out ExtrudedSymbolDescription overlayDescription,
          out ExtrudedSymbolDescription featureDescription,
          out SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId);

      var patternTile = terrain.pattern.patternTiles[location.indexInGroup];

      string symbolName = "a";
      switch (terrain.pattern.name) {
        case "square":
          if (patternTile.shapeIndex == 0) {
            symbolName = "j";
          }
          break;
        case "pentagon9":
          if (patternTile.shapeIndex == 0) {
            symbolName = "0";
          } else if (patternTile.shapeIndex == 1) {
            symbolName = "1";
          }
          break;
      }

      ExtrudedSymbolDescription tileSymbolDescription =
          new ExtrudedSymbolDescription(
              RenderPriority.TILE,
              new SymbolDescription(
                  symbolName,
                  topColor,
                  0,
                  OutlineMode.WithOutline,
                  outlineColor),
              true,
              sideColor);

      TileDescription description =
          new TileDescription(
              terrain.elevationStepHeight,
              patternTile.rotateDegrees,
              neededDepth,
              tileSymbolDescription,
              overlayDescription,
              featureDescription,
              itemSymbolDescriptionByItemId);
      return description;
    }

    public void DestroyTerrainTilePresenter() {
      if (terrainTile.Exists()) {
        componentsBroadcaster.RemoveObserver(this);
        componentsBroadcaster.Stop();
      }

      tileView.DestroyTile();
    }

    private static void DetermineTileAppearance(
        TerrainTile terrainTile,
        out Color topColor,
        out Color outlineColor,
        out Color sideColor,
        out ExtrudedSymbolDescription overlay,
        out ExtrudedSymbolDescription feature,
        out SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId) {
      bool topColorLocked = false;
      topColor = new UnityEngine.Color(1.0f, 0, 1.0f);

      bool outlineColorLocked = false;
      outlineColor = new UnityEngine.Color(0f, 0f, 0f);

      bool sideColorLocked = false;
      sideColor = new UnityEngine.Color(1.0f, 0, 1.0f);

      bool overlayLocked = false;
      overlay = null;

      bool featureLocked = false;
      feature = null;

      itemSymbolDescriptionByItemId = new SortedDictionary<int, ExtrudedSymbolDescription>();

      switch (terrainTile.classId) {
        //case "grass":
          //if (terrainTile.elevation == 1) {
          //  topColor = new UnityEngine.Color(0, .3f, 0);
          //  sideColor = new UnityEngine.Color(0, .2f, 0);
          //} else if (terrainTile.elevation == 2) {
          //  topColor = new UnityEngine.Color(0, .43f, 0);
          //  sideColor = new UnityEngine.Color(0, .3f, 0);
          //}
          //break;
        case "grass":
          if (terrainTile.elevation == 1) {
            topColor = new UnityEngine.Color(.3f, .15f, 0);
            sideColor = new UnityEngine.Color(.2f, .1f, 0);
          } else if (terrainTile.elevation == 2) {
            topColor = new UnityEngine.Color(.43f, .21f, 0);
            sideColor = new UnityEngine.Color(.3f, .15f, 0);
          }
          break;
        case "stone":
          if (terrainTile.elevation == 1) {
            topColor = new UnityEngine.Color(.2f, .2f, .2f);
            sideColor = new UnityEngine.Color(.15f, .15f, .15f);
          } else if (terrainTile.elevation == 2) {
            topColor = new UnityEngine.Color(.3f, .3f, .3f);
            sideColor = new UnityEngine.Color(.2f, .2f, .2f);
          }
          break;
        //case "clifflanding":
          //topColor = new UnityEngine.Color(.4f, .2f, 0f);
          //outlineColor = new Color(.6f, 0.4f, .15f);
          //sideColor = new UnityEngine.Color(.2f, .1f, 0f);
          //break;
        case "clifflanding":
          topColor = new UnityEngine.Color(.2f, .2f, .2f);
          sideColor = new UnityEngine.Color(.1f, .05f, 0f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
          break;
        case "ravanest":
          topColor = new UnityEngine.Color(.2f, 0, .2f);
          sideColor = new UnityEngine.Color(.2f, 0f, .2f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
          break;
        case "cliff":
          topColor = new UnityEngine.Color(.2f, .1f, 0f);
          sideColor = new UnityEngine.Color(.1f, .05f, 0f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
          break;
        case "magma":
          topColor = new UnityEngine.Color(.4f, 0f, 0f);
          outlineColor = new Color(.5f, 0f, 0.0f);
          sideColor = new UnityEngine.Color(.2f, 0f, 0f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "f",
                      new Color(.5f, .1f, 0f),
                      0,
                      OutlineMode.WithOutline,
                      new Color(.5f, .1f, 0.0f)),
                  false,
                  new Color(0, 0, 0));
          break;
        case "falls":
          topColor = new UnityEngine.Color(.2f, .3f, 1.0f);
          outlineColor = new Color(0f, 0f, 1.0f);
          sideColor = new UnityEngine.Color(.2f, .3f, 1.0f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "o",
                      new Color(.3f, .4f, 1.0f),
                      0,
                      OutlineMode.NoOutline,
                      new Color(0f, 0f, 1.0f)),
                  false,
                  new Color(0, 0, 0));
          break;
      }

      foreach (var ttc in terrainTile.components) {
        if (ttc is DecorativeTTCAsITerrainTileComponent decorationI) {
          var decoration = decorationI.obj;
          switch (decoration.symbolId) {
            case "rocks":
              if (!overlayLocked) {
                overlay =
                    new ExtrudedSymbolDescription(
                        RenderPriority.SYMBOL,
                        new SymbolDescription(
                            "f",
                            new Color(1f, 1f, 1f, .1f),
                            0,
                            OutlineMode.WithOutline,
                            new Color(0, 0, 0)),
                        false,
                        new Color(0, 0, 0));
              }
              break;
            case "blood":
              if (!overlayLocked) {
                overlay =
                    new ExtrudedSymbolDescription(
                        RenderPriority.SYMBOL,
                      new SymbolDescription(
                            "g",
                            new Color(1f, 0, 0, .3f),
                            0,
                            OutlineMode.WithOutline,
                            new Color(0, 0, 0)),
                        false,
                        new Color(0, 0, 0));
              }
              break;
            case "downstairs":
              topColor = new Color(0, 0, 0);
              topColorLocked = true;

              overlay =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "d",
                          new Color(.5f, .5f, .5f, 1f),
                          0,
                          OutlineMode.WithOutline,
                          new Color(0, 0, 0)),
                      false,
                      new Color(0, 0, 0));
              overlayLocked = true;
              break;
            case "upstairs":
              feature =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "c",
                          new Color(1f, 1f, 1f),
                          0,
                          OutlineMode.WithOutline,
                          new Color(0, 0, 0)),
                      true,
                      new Color(1f, 1f, 1f));
              featureLocked = true;
              break;
            case "cave":
              feature =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "p",
                          new Color(0, 0, 0),
                          0,
                          OutlineMode.WithOutline,
                          new Color(1, 1, 1)),
                      false,
                      new Color(1f, 1f, 1f));
              featureLocked = true;
              break;
          }
        } else if (ttc is StaircaseTTCAsITerrainTileComponent) {
        } else if (ttc is ArmorAsITerrainTileComponent) {
          itemSymbolDescriptionByItemId.Add(
              ttc.id,
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "0",
                      new Color(1f, 1f, 1.0f, 1.5f),
                      0,
                      OutlineMode.WithBackOutline,
                      new Color(0, 0, 0)),
                  true,
                  new Color(.75f, .75f, 0)));
        } else if (ttc is GlaiveAsITerrainTileComponent) {
          itemSymbolDescriptionByItemId.Add(
              ttc.id,
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "s",
                      new Color(1f, 1f, 1f, 1.5f),
                      0,
                      OutlineMode.WithBackOutline,
                      new Color(0, 0, 0)),
                  true,
                  new Color(.5f, 0f, 0)));
        } else if (ttc is InertiaRingAsITerrainTileComponent) {
          itemSymbolDescriptionByItemId.Add(
              ttc.id,
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "4",
                      new Color(1f, 1f, 1f, 1.5f),
                      0,
                      OutlineMode.WithBackOutline,
                      new Color(0, 0, 0)),
                  true,
                  new Color(.5f, 0f, 0)));
        } else if (ttc is HealthPotionAsITerrainTileComponent) {
          itemSymbolDescriptionByItemId.Add(
              ttc.id,
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "+",
                      new Color(.8f, 0, .8f, 1.5f),
                      0,
                      OutlineMode.WithBackOutline,
                      new Color(0, 0, 0)),
                  true,
                  new Color(0f, 0f, 0)));
        } else if (ttc is TimeAnchorTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "l",
                        new Color(1.0f, 1.0f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new Color(0, 0, 0)),
                    true,
                    new Color(0f, 0f, 0));
          }
        } else if (ttc is ManaPotionAsITerrainTileComponent) {
          itemSymbolDescriptionByItemId.Add(
              ttc.id,
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      ",",
                      new Color(.25f, .7f, 1.0f, 1.5f),
                      0,
                      OutlineMode.WithBackOutline,
                      new Color(0, 0, 0)),
                  true,
                  new Color(0f, 0f, 0)));
        } else {
          Asserts.Assert(false, ttc.ToString());
        }
      }
    }

    public void OnMouseClick() {
      foreach (var observer in observers) {
        observer.OnMouseClick(location);
      }
    }

    public void OnMouseEnter() {
      foreach (var observer in observers) {
        observer.OnMouseIn(location);
      }
    }

    public void OnMouseExit() {
      foreach (var observer in observers) {
        observer.OnMouseOut(location);
      }
    }

    public void OnITerrainTileComponentMutBunchAdd(int id) {
      tileView.SetDescription(GetDescription());
    }

    public void OnITerrainTileComponentMutBunchRemove(int id) {
      tileView.SetDescription(GetDescription());
    }
  }
}