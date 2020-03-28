using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia;
using Atharia.Model;
using UnityEngine;
using Domino;

namespace AthPlayer {
  public class TerrainTilePresenterTile : MonoBehaviour {
    // PhantomTilePresenter attaches this to the TileView it creates, so that when EditorPresenter
    // raycasts, it can know the PhantomTilePresenter that owns this TileView.
    // This approach is an implementation detail of the Editor, and shouldnt enter Domino.
    public TerrainTilePresenter presenter;

    public void Init(TerrainTilePresenter presenter) {
      this.presenter = presenter;
    }
  }

  public class TerrainTilePresenter :
      IITerrainTileComponentMutBunchObserver,
    ITerrainTileEffectObserver,
    ITerrainTileEffectVisitor {
    EffectBroadcaster broadcaster;
  Atharia.Model.Terrain terrain;
    public readonly Location location;
    TerrainTile terrainTile;
    Instantiator instantiator;

    TileView tileView;

    ITerrainTileComponentMutBunchBroadcaster componentsBroadcaster;

    bool highlighted = false;

    public TerrainTilePresenter(
      IClock clock,
      ITimer timer,
      EffectBroadcaster broadcaster,
        Atharia.Model.Terrain terrain,
        Location location,
        TerrainTile terrainTile,
        Instantiator instantiator) {
      this.location = location;
      this.terrain = terrain;
      this.broadcaster = broadcaster;
      this.terrainTile = terrainTile;
      this.instantiator = instantiator;

      var positionVec2 = terrain.pattern.GetTileCenter(location);
      tileView =
          instantiator.CreateTileView(
            clock,
              timer,
              new UnityEngine.Vector3(
                  positionVec2.x,
                  terrainTile.elevation * terrain.elevationStepHeight,
                  positionVec2.y),
              GetDescription());
      tileView.gameObject.AddComponent<TerrainTilePresenterTile>().Init(this);

      terrainTile.AddObserver(broadcaster, this);
      componentsBroadcaster = new ITerrainTileComponentMutBunchBroadcaster(broadcaster, terrainTile.components);
      componentsBroadcaster.AddObserver(this);

      tileView.SetDescription(GetDescription());
    }

    public void SetHighlighted(bool highlighted) {
      this.highlighted = highlighted;
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
          out UnityEngine.Color topColor,
          out UnityEngine.Color outlineColor,
          out UnityEngine.Color sideColor,
          out ExtrudedSymbolDescription overlayDescription,
          out ExtrudedSymbolDescription featureDescription,
          out SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId);

      var patternTile = terrain.pattern.patternTiles[location.indexInGroup];

      string symbolName = "a";
      switch (terrain.pattern.name) {
        case "square":
          if (patternTile.shapeIndex == 0) {
            symbolName = "six";
          }
          break;
        case "pentagon9":
          if (patternTile.shapeIndex == 0) {
            symbolName = "i";
          } else if (patternTile.shapeIndex == 1) {
            symbolName = "h";
          }
          break;
        case "hex":
          if (patternTile.shapeIndex == 0) {
            symbolName = "five";
          }
          break;
      }

      ExtrudedSymbolDescription tileSymbolDescription =
          new ExtrudedSymbolDescription(
              RenderPriority.TILE,
              new SymbolDescription(
                  symbolName,
                  100,
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

      if (highlighted) {
        var frontColor = (description.tileSymbolDescription.symbol.frontColor * 7 + new UnityEngine.Color(1, 1, 1)) / 8;
        var sidesColor = (description.tileSymbolDescription.sidesColor * 5 + new UnityEngine.Color(1, 1, 1)) / 6;
        description =
          description.WithTileSymbolDescription(
            description.tileSymbolDescription
              .WithSymbol(description.tileSymbolDescription.symbol.WithFrontColor(frontColor))
              .WithSidesColor(sidesColor));
      }

      return description;
    }

    public void DestroyTerrainTilePresenter() {
      if (terrainTile.Exists()) {
        terrainTile.RemoveObserver(broadcaster, this);
        componentsBroadcaster.RemoveObserver(this);
        componentsBroadcaster.Stop();
      }

      tileView.DestroyTile();
    }

    private static void DetermineTileAppearance(
        TerrainTile terrainTile,
        out UnityEngine.Color topColor,
        out UnityEngine.Color outlineColor,
        out UnityEngine.Color sideColor,
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

      foreach (var ttc in terrainTile.components) {

        // someday, we should have these if-else cases just call into a MemberToViewMapper...

        if (ttc is GrassTTCAsITerrainTileComponent) {
            topColor = new UnityEngine.Color(0f, .3f, 0);
            sideColor = new UnityEngine.Color(0f, .2f, 0);
        } else if (ttc is StoneTTCAsITerrainTileComponent) {
          if (terrainTile.elevation == 1) {
            topColor = new UnityEngine.Color(.2f, .2f, .2f);
            sideColor = new UnityEngine.Color(.15f, .15f, .15f);
          } else if (terrainTile.elevation == 2) {
            topColor = new UnityEngine.Color(.3f, .3f, .3f);
            sideColor = new UnityEngine.Color(.2f, .2f, .2f);
          }
        } else if (ttc is DirtTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.4f, .133f, 0);
          sideColor = new UnityEngine.Color(.266f, .1f, 0);
        } else if (ttc is MudTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.35f, .11f, 0f);
          sideColor = new UnityEngine.Color(.23f, .08f, 0f);
        } else if (ttc is CaveWallTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.24f, .08f, 0f);
          sideColor = new UnityEngine.Color(.16f, .05f, 0f);
        } else if (ttc is CliffLandingTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.2f, .2f, .2f);
          sideColor = new UnityEngine.Color(.1f, .05f, 0f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
        } else if (ttc is ObsidianFloorTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.1f, .1f, .05f);
          sideColor = new UnityEngine.Color(.05f, .05f, .05f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
        } else if (ttc is RavaNestTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.2f, 0, .2f);
          sideColor = new UnityEngine.Color(.2f, 0f, .2f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
        } else if (ttc is CliffTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.2f, .1f, 0f);
          sideColor = new UnityEngine.Color(.1f, .05f, 0f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
        } else if (ttc is WaterTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.2f, .3f, 1.0f);
          outlineColor = new UnityEngine.Color(0f, 0f, 1.0f);
          sideColor = new UnityEngine.Color(.2f, .3f, 1.0f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "o",
                      50,
                      new UnityEngine.Color(.3f, .4f, 1.0f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0f, 0f, 1.0f)),
                  false,
                  new UnityEngine.Color(0, 0, 0));
        } else if (ttc is FloorTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.2f, .2f, .2f);
          sideColor = new UnityEngine.Color(.15f, .15f, .15f);
          outlineColor = new UnityEngine.Color(0f, 0f, 0f);
        } else if (ttc is EmberDeepLevelLinkerTTCAsITerrainTileComponent) {
        } else if (ttc is MagmaTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.4f, 0f, 0f);
          outlineColor = new UnityEngine.Color(.2f, 0f, 0.0f);
          sideColor = new UnityEngine.Color(.2f, 0f, 0f);
          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "f-3",
                      50,
                      new UnityEngine.Color(.5f, .1f, 0f),
                      0,
                      OutlineMode.WithOutline,
                      new UnityEngine.Color(.5f, .1f, 0.0f)),
                  false,
                  new UnityEngine.Color(0, 0, 0));
        } else if (ttc is FallsTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(.2f, .3f, 1.0f);
          outlineColor = new UnityEngine.Color(0f, 0f, 1.0f);
          sideColor = new UnityEngine.Color(.2f, .3f, 1.0f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "o",
                      50,
                      new UnityEngine.Color(.3f, .4f, 1.0f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0f, 0f, 1.0f)),
                  false,
                  new UnityEngine.Color(0, 0, 0));
        } else if (ttc is RocksTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "f-3",
                        50,
                        new UnityEngine.Color(1f, 1f, 1f, .1f),
                        0,
                        OutlineMode.WithOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    false,
                    new UnityEngine.Color(0, 0, 0));
          }
        } else if (ttc is FireBombTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "k",
                        50,
                        new UnityEngine.Color(1.0f, 0.5f, 0f, 1.0f),
                        0,
                        OutlineMode.WithOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    false,
                    new UnityEngine.Color(0, 0, 0));
            overlayLocked = true;
          }
        } else if (ttc is KamikazeTargetTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "k",
                        50,
                        new UnityEngine.Color(1.0f, 0f, 0f, 0.5f),
                        0,
                        OutlineMode.WithOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    false,
                    new UnityEngine.Color(0, 0, 0));
            overlayLocked = true;
          }
        } else if (ttc is ObsidianTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f-3",
                            50, new UnityEngine.Color(0f, 0f, 0f, .8f), 0, OutlineMode.WithOutline, new UnityEngine.Color(0, 0, 0)),
            false,
            new UnityEngine.Color(0, 0, 0));
          }
        } else if (ttc is BloodTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                  new SymbolDescription(
                        "g",
                        50,
                        new UnityEngine.Color(1f, 0, 0, .3f),
                        0,
                        OutlineMode.WithOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    false,
                    new UnityEngine.Color(0, 0, 0));
          }
        } else if (ttc is DownStairsTTCAsITerrainTileComponent) {
          topColor = new UnityEngine.Color(0, 0, 0);
          topColorLocked = true;

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "d",
                        100,
                      new UnityEngine.Color(.5f, .5f, .5f, 1f),
                      0,
                      OutlineMode.WithOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false,
                  new UnityEngine.Color(0, 0, 0));
          overlayLocked = true;
        } else if (ttc is UpStairsTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "c",
                        100,
                      new UnityEngine.Color(1f, 1f, 1f),
                      0,
                      OutlineMode.WithOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  true,
                  new UnityEngine.Color(1f, 1f, 1f));
          featureLocked = true;
        } else if (ttc is TreeTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "n",
                        50,
                      new UnityEngine.Color(0, .5f, 0),
                      0,
                      OutlineMode.WithOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  false,
                  new UnityEngine.Color(0f, .3f, 0f));
        } else if (ttc is CaveTTCAsITerrainTileComponent) {
              feature =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "p",
                            50,
                          new UnityEngine.Color(0, 0, 0),
                          0,
                          OutlineMode.WithOutline,
                          new UnityEngine.Color(1, 1, 1)),
                      false,
                      new UnityEngine.Color(1f, 1f, 1f));
              featureLocked = true;
        } else if (ttc is FireTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "r-3",
                            50,
                          new UnityEngine.Color(.3f, .1f, 0, 1.2f),
                          0,
                          OutlineMode.WithOutline,
                          new UnityEngine.Color(0, 0, 0)),
                      false,
                      new UnityEngine.Color(0f, .3f, 0f));
        } else if (ttc is LevelLinkTTCAsITerrainTileComponent) {
        } else if (ttc is WarperTTCAsITerrainTileComponent) {
        } else if (ttc is IncendianFallsLevelLinkerTTCAsITerrainTileComponent) {
        } else if (ttc is WallTTCAsITerrainTileComponent) {
        } else if (ttc is MarkerTTCAsITerrainTileComponent) {
        } else if (ttc is ItemTTCAsITerrainTileComponent itemTTC) {
          var item = itemTTC.obj.item;
          if (item is ArmorAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "zero",
                              50,
                        new UnityEngine.Color(1f, 1f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(.75f, .75f, 0)));
          } else if (item is BlastRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                              50,
                        new UnityEngine.Color(1f, .5f, 0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(.75f, .75f, 0)));
          } else if (item is SlowRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                              50,
                        new UnityEngine.Color(0f, .5f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(.75f, .75f, 0)));
          } else if (item is GlaiveAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "s",
                              50,
                        new UnityEngine.Color(1f, 1f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(.5f, 0f, 0)));
          } else if (item is SpeedRingAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "four",
                              50,
                        new UnityEngine.Color(1f, 1f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(.5f, 0f, 0)));
          } else if (item is HealthPotionAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "plus",
                              50,
                        new UnityEngine.Color(.8f, 0, .8f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(0f, 0f, 0)));
          } else if (item is ManaPotionAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "comma",
                              50,
                        new UnityEngine.Color(.25f, .7f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(0f, 0f, 0)));
          } else {
            Asserts.Assert(false, "Found item: " + ttc);
          }
        } else if (ttc is SimplePresenceTriggerTTCAsITerrainTileComponent) {
        } else if (ttc is TimeAnchorTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "k",
                            50,
                        new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(0f, 0f, 0));
            overlayLocked = true;
          }
        } else {
          Asserts.Assert(false, ttc.ToString());
        }
      }
    }

    public void OnITerrainTileComponentMutBunchAdd(int id) {
      tileView.SetDescription(GetDescription());
    }

    public void OnITerrainTileComponentMutBunchRemove(int id) {
      tileView.SetDescription(GetDescription());
    }

    public void OnTerrainTileEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this);  }
    public void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect) { }
    public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect) { }
    public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect) { }
    public void visitTerrainTileSetEvventEffect(TerrainTileSetEvventEffect effect) {
      if (effect.newValue is UnitUnleashBideEventAsITerrainTileEvent) {
        tileView.ShowRune(
            new ExtrudedSymbolDescription(
                RenderPriority.RUNE,
                new SymbolDescription(
                    "r-3",
                            50,
                      new UnityEngine.Color(1.0f, .6f, 0, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    new UnityEngine.Color(0, 0, 0)),
                true,
                new UnityEngine.Color(0, 0, 1f, 1f)));
      } else if (effect.newValue is UnitFireBombedEventAsITerrainTileEvent ufbe) {
        tileView.ShowRune(
            new ExtrudedSymbolDescription(
                RenderPriority.RUNE,
                new SymbolDescription(
                    "r-3",
                            50,
                      new UnityEngine.Color(1.0f, .6f, 0, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    new UnityEngine.Color(0, 0, 0)),
                true,
                new UnityEngine.Color(0, 0, 1f, 1f)));
      }
    }
  }
}
