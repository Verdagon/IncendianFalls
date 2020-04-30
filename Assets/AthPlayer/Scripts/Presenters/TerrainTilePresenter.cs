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

    EffectBroadcaster preBroadcaster;
    EffectBroadcaster postBroadcaster;
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
        EffectBroadcaster preBroadcaster,
        EffectBroadcaster postBroadcaster,
        Atharia.Model.Terrain terrain,
        Location location,
        TerrainTile terrainTile,
        Instantiator instantiator) {
      this.location = location;
      this.terrain = terrain;
      this.preBroadcaster = preBroadcaster;
      this.postBroadcaster = postBroadcaster;
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

      terrainTile.AddObserver(postBroadcaster, this);
      componentsBroadcaster = new ITerrainTileComponentMutBunchBroadcaster(postBroadcaster, terrainTile.components);
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
          out Vector4Animation topColor,
          out Vector4Animation outlineColor,
          out Vector4Animation sideColor,
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

        var frontColor =
          new MultiplyVector4Animation(
            new AddVector4Animation(
              new MultiplyVector4Animation(description.tileSymbolDescription.symbol.frontColor, 7f),
              new MultiplyVector4Animation(Vector4Animation.Color(1, 1, 1, 1), 1f)),
            1 / 8f);
        var sidesColor =
          new MultiplyVector4Animation(
            new AddVector4Animation(
              new MultiplyVector4Animation(description.tileSymbolDescription.sidesColor, 5f),
              new MultiplyVector4Animation(Vector4Animation.Color(1, 1, 1, 1), 1f)),
            1 / 6f);

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
        terrainTile.RemoveObserver(postBroadcaster, this);
        componentsBroadcaster.RemoveObserver(this);
        componentsBroadcaster.Stop();
      }

      tileView.DestroyTile();
    }

    private static void DetermineTileAppearance(
        TerrainTile terrainTile,
        out Vector4Animation topColor,
        out Vector4Animation outlineColor,
        out Vector4Animation sideColor,
        out ExtrudedSymbolDescription overlay,
        out ExtrudedSymbolDescription feature,
        out SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId) {

      bool topColorLocked = false;
      topColor = Vector4Animation.Color(1.0f, 0, 1.0f);

      bool outlineColorLocked = false;
      outlineColor = Vector4Animation.Color(0f, 0f, 0f);

      bool sideColorLocked = false;
      sideColor = Vector4Animation.Color(1.0f, 0, 1.0f);

      bool overlayLocked = false;
      overlay = null;

      bool featureLocked = false;
      feature = null;

      itemSymbolDescriptionByItemId = new SortedDictionary<int, ExtrudedSymbolDescription>();

      foreach (var ttc in terrainTile.components) {

        // someday, we should have these if-else cases just call into a MemberToViewMapper...

        if (ttc is GrassTTCAsITerrainTileComponent) {
            topColor = Vector4Animation.Color(0f, .3f, 0);
            sideColor = Vector4Animation.Color(0f, .2f, 0);
        } else if (ttc is StoneTTCAsITerrainTileComponent) {
          if (terrainTile.elevation == 1) {
            topColor = Vector4Animation.Color(.2f, .2f, .2f);
            sideColor = Vector4Animation.Color(.15f, .15f, .15f);
          } else if (terrainTile.elevation == 2) {
            topColor = Vector4Animation.Color(.3f, .3f, .3f);
            sideColor = Vector4Animation.Color(.2f, .2f, .2f);
          }
        } else if (ttc is DirtTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.4f, .133f, 0);
          sideColor = Vector4Animation.Color(.266f, .1f, 0);
        } else if (ttc is MudTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.35f, .11f, 0f);
          sideColor = Vector4Animation.Color(.23f, .08f, 0f);
        } else if (ttc is CaveWallTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.24f, .08f, 0f);
          sideColor = Vector4Animation.Color(.16f, .05f, 0f);
        } else if (ttc is CliffLandingTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.2f, .2f, .2f);
          sideColor = Vector4Animation.Color(.1f, .05f, 0f);
          outlineColor = Vector4Animation.Color(0f, 0f, 0f);
        } else if (ttc is ObsidianFloorTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.1f, .1f, .05f);
          sideColor = Vector4Animation.Color(.05f, .05f, .05f);
          outlineColor = Vector4Animation.Color(0f, 0f, 0f);
        } else if (ttc is RavaNestTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.2f, 0, .2f);
          sideColor = Vector4Animation.Color(.2f, 0f, .2f);
          outlineColor = Vector4Animation.Color(0f, 0f, 0f);
        } else if (ttc is CliffTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.2f, .1f, 0f);
          sideColor = Vector4Animation.Color(.1f, .05f, 0f);
          outlineColor = Vector4Animation.Color(0f, 0f, 0f);
        } else if (ttc is WaterTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.2f, .3f, 1.0f);
          outlineColor = Vector4Animation.Color(0f, 0f, 1.0f);
          sideColor = Vector4Animation.Color(.2f, .3f, 1.0f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "o",
                      Vector4Animation.Color(.3f, .4f, 1.0f),
                      0,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0f, 0f, 1.0f)),
                  false,
                  Vector4Animation.Color(0, 0, 0));
        } else if (ttc is FloorTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.2f, .2f, .2f);
          sideColor = Vector4Animation.Color(.15f, .15f, .15f);
          outlineColor = Vector4Animation.Color(0f, 0f, 0f);
        } else if (ttc is EmberDeepLevelLinkerTTCAsITerrainTileComponent) {
        } else if (ttc is MagmaTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.4f, 0f, 0f);
          outlineColor = Vector4Animation.Color(.2f, 0f, 0.0f);
          sideColor = Vector4Animation.Color(.2f, 0f, 0f);
          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "f-3",
                      Vector4Animation.Color(.5f, .1f, 0f),
                      0,
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(.5f, .1f, 0.0f)),
                  false,
                  Vector4Animation.Color(0, 0, 0));
        } else if (ttc is FallsTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.2f, .3f, 1.0f);
          outlineColor = Vector4Animation.Color(0f, 0f, 1.0f);
          sideColor = Vector4Animation.Color(.2f, .3f, 1.0f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "o",
                      Vector4Animation.Color(.3f, .4f, 1.0f),
                      0,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0f, 0f, 1.0f)),
                  false,
                  Vector4Animation.Color(0, 0, 0));
        } else if (ttc is RocksTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "f-3",
                        Vector4Animation.Color(1f, 1f, 1f, .1f),
                        0,
                        OutlineMode.WithOutline),
                    false,
                    Vector4Animation.Color(0, 0, 0));
          }
        } else if (ttc is FireBombTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "k",
                        Vector4Animation.Color(1.0f, 0.5f, 0f, 1.0f),
                        0,
                        OutlineMode.WithOutline),
                    false,
                    Vector4Animation.Color(0, 0, 0));
            overlayLocked = true;
          }
        } else if (ttc is KamikazeTargetTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "k",
                        Vector4Animation.Color(1.0f, 0f, 0f, 0.5f),
                        0,
                        OutlineMode.WithOutline),
                    false,
                    Vector4Animation.Color(0, 0, 0));
            overlayLocked = true;
          }
        } else if (ttc is ObsidianTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f-3", Vector4Animation.Color(0f, 0f, 0f, .8f), 0, OutlineMode.WithOutline, Vector4Animation.Color(0, 0, 0)),
            false,
            Vector4Animation.Color(0, 0, 0));
          }
        } else if (ttc is BloodTTCAsITerrainTileComponent) {
          if (!overlayLocked) {
            overlay =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                  new SymbolDescription(
                        "g",
                        Vector4Animation.Color(1f, 0, 0, .3f),
                        0,
                        OutlineMode.WithOutline),
                    false,
                    Vector4Animation.Color(0, 0, 0));
          }
        } else if (ttc is DownStairsTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(0, 0, 0);
          topColorLocked = true;

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "d",
                      Vector4Animation.Color(.5f, .5f, .5f, 1f),
                      0,
                      OutlineMode.WithOutline),
                  false,
                  Vector4Animation.Color(0, 0, 0));
          overlayLocked = true;
        } else if (ttc is UpStairsTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "c",
                      Vector4Animation.Color(1f, 1f, 1f),
                      0,
                      OutlineMode.WithOutline),
                  true,
                  Vector4Animation.Color(1f, 1f, 1f));
          featureLocked = true;
        } else if (ttc is TreeTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "n",
                      Vector4Animation.Color(0, .5f, 0),
                      0,
                      OutlineMode.WithOutline),
                  false,
                  Vector4Animation.Color(0f, .3f, 0f));
        } else if (ttc is CaveTTCAsITerrainTileComponent) {
              feature =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "p",
                          Vector4Animation.Color(0, 0, 0),
                          0,
                          OutlineMode.WithOutline,
                          Vector4Animation.Color(1, 1, 1)),
                      false,
                      Vector4Animation.Color(1f, 1f, 1f));
              featureLocked = true;
        } else if (ttc is FireTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "r-3",
                          Vector4Animation.Color(.3f, .1f, 0, 1.2f),
                          0,
                          OutlineMode.WithOutline,
                          Vector4Animation.Color(0, 0, 0)),
                      false,
                      Vector4Animation.Color(0f, .3f, 0f));
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
                        Vector4Animation.Color(1f, 1f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(.75f, .75f, 0)));
          } else if (item is BlastRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(1f, .5f, 0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(.75f, .75f, 0)));
          } else if (item is SlowRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(0f, .5f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(.75f, .75f, 0)));
          } else if (item is GlaiveAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "s",
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(.5f, 0f, 0)));
          } else if (item is SpeedRingAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "four",
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(.5f, 0f, 0)));
          } else if (item is HealthPotionAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "plus",
                        Vector4Animation.Color(.8f, 0, .8f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(0f, 0f, 0)));
          } else if (item is ManaPotionAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "comma",
                        Vector4Animation.Color(.25f, .7f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(0f, 0f, 0)));
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
                        Vector4Animation.Color(1.0f, 1.0f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(0f, 0f, 0));
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
                      Vector4Animation.Color(1.0f, .6f, 0, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    Vector4Animation.Color(0, 0, 0)),
                true,
                Vector4Animation.Color(0, 0, 1f, 1f)));
      } else if (effect.newValue is UnitFireBombedEventAsITerrainTileEvent ufbe) {
        tileView.ShowRune(
            new ExtrudedSymbolDescription(
                RenderPriority.RUNE,
                new SymbolDescription(
                    "r-3",
                      Vector4Animation.Color(1.0f, .6f, 0, 1.5f),
                    0,
                    OutlineMode.WithOutline,
                    Vector4Animation.Color(0, 0, 0)),
                true,
                Vector4Animation.Color(0, 0, 1f, 1f)));
      }
    }
  }
}
