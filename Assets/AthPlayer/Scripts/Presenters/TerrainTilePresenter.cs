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
    public delegate void IOnAnimation(long endGameTimeMs);
    public event IOnAnimation onAnimation;

    IClock clock;
    EffectBroadcaster preBroadcaster;
    EffectBroadcaster postBroadcaster;
    Atharia.Model.Terrain terrain;
    public readonly Location location;
    TerrainTile terrainTile;
    Instantiator instantiator;

    TileView tileView;

    ITerrainTileComponentMutBunchBroadcaster componentsBroadcaster;

    bool highlighted = false;

    // We dont want two prisms to appear at the same time.
    long prismEndTime;

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
      this.clock = clock;
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

    private string GetTerrainTileShapeSymbol(PatternTile patternTile) {
      switch (terrain.pattern.name) {
        case "square":
          if (patternTile.shapeIndex == 0) {
            return "six";
          }
          break;
        case "pentagon9":
          if (patternTile.shapeIndex == 0) {
            return "i";
          } else if (patternTile.shapeIndex == 1) {
            return "h";
          }
          break;
        case "hex":
          if (patternTile.shapeIndex == 0) {
            return "five";
          }
          break;
      }
      return "a";
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
          location,
          out Vector4Animation topColor,
          out Vector4Animation outlineColor,
          out Vector4Animation sideColor,
          out ExtrudedSymbolDescription overlayDescription,
          out ExtrudedSymbolDescription featureDescription,
          out SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId);

      var patternTile = terrain.pattern.patternTiles[location.indexInGroup];

      string symbolName = GetTerrainTileShapeSymbol(patternTile);

      ExtrudedSymbolDescription tileSymbolDescription =
          new ExtrudedSymbolDescription(
              RenderPriority.TILE,
              new SymbolDescription(
                  symbolName,
                  topColor,
                  0,
                  1,
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

    public (long, TileView) DestroyTerrainTilePresenter() {
      if (terrainTile.Exists()) {
        terrainTile.RemoveObserver(postBroadcaster, this);
        componentsBroadcaster.RemoveObserver(this);
        componentsBroadcaster.Stop();
      }

      long animationsEndTime =
        //Math.Max(hopEndTime,
        //  Math.Max(lungeEndTime,
        //    Math.Max(runeEndTime,
        //      dieEndTime)));
        prismEndTime;
      if (clock.GetTimeMs() >= animationsEndTime) {
        tileView.DestroyTile();
        tileView = null;
      }

      //instanceAlive = false;
      return (animationsEndTime, tileView);
    }

    private static void DetermineTileAppearance(
        TerrainTile terrainTile,
        Location location,
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
          topColor = Vector4Animation.Color(0f, .1f + 0.0333f * terrainTile.elevation, 0);
          sideColor = Vector4Animation.Color(.15f, .1f, .05f);

          var rand = new System.Random(location.groupX * 177 + location.groupY * 131 + location.indexInGroup * 91 + terrainTile.elevation * 79);
          if (rand.Next() < 40) {
            if (!overlayLocked) {
              overlay =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          (new[] {"grass1", "grass2", "grass3"})[rand.Next() % 3],
                          Vector4Animation.Color(0f, .2f + 0.0333f * terrainTile.elevation, 0),
                          0,
                          1,
                          OutlineMode.NoOutline,
                          Vector4Animation.Color(0f, 0f, 0.5f)),
                      false,
                      Vector4Animation.Color(0, 0, 0));
            }
          }
        } else if (ttc is LeafTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(0f, .1f + 0.0333f * terrainTile.elevation, 0);
          sideColor = Vector4Animation.Color(.15f, .1f, .05f);

          if (!featureLocked) {
            var rand = new System.Random(location.groupX * 177 + location.groupY * 131 + location.indexInGroup * 91 + terrainTile.elevation * 79);
            feature =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        (new[] {"7", "8", "9"})[rand.Next() % 3],
                        Vector4Animation.Color(0, .2f + 0.0333f * terrainTile.elevation, 0),
                        0,
                        1,
                        OutlineMode.WithOutline),
                    false,
                    Vector4Animation.Color(0f, 0f, .5f));
          }
        } else if (ttc is FlowerTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(0f, .1f + 0.0333f * terrainTile.elevation, 0);
          sideColor = Vector4Animation.Color(.15f, .1f, .05f);

          if (!featureLocked) {
            feature =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "flower",
                        Vector4Animation.Color(0.547f, .4f, .547f),
                        0,
                        0.75f,
                        OutlineMode.WithBackOutline,
                        Vector4Animation.Color(0, 0, 0)),
                    false,
                    Vector4Animation.Color(0f, 0f, .5f));
          }
        } else if (ttc is LotusTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(0f, .1f + 0.0333f * terrainTile.elevation, 0);
          sideColor = Vector4Animation.Color(.15f, .1f, .05f);

          if (!featureLocked) {
            feature =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "lotus",
                        Vector4Animation.Color(.1f, .5f, .5f),
                        0,
                        0.75f,
                        OutlineMode.WithBackOutline,
                        Vector4Animation.Color(0, 0, 0)),
                    false,
                    Vector4Animation.Color(0f, 0f, .5f));
          }
        } else if (ttc is RoseTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(0f, .1f + 0.0333f * terrainTile.elevation, 0);
          sideColor = Vector4Animation.Color(.15f, .1f, .05f);

          if (!featureLocked) {
            feature =
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "rose",
                        Vector4Animation.Color(.8f, .4f, 0.28f),
                        0,
                        0.75f,
                        OutlineMode.WithBackOutline,
                        Vector4Animation.Color(0, 0, 0)),
                    false,
                    Vector4Animation.Color(0f, 0f, .5f));
          }
        } else if (ttc is StoneTTCAsITerrainTileComponent) {
          if (terrainTile.elevation == 1) {
            topColor = Vector4Animation.Color(.2f, .2f, .2f);
            sideColor = Vector4Animation.Color(.15f, .15f, .15f);
          } else if (terrainTile.elevation == 2) {
            topColor = Vector4Animation.Color(.3f, .3f, .3f);
            sideColor = Vector4Animation.Color(.2f, .2f, .2f);
          }
        } else if (ttc is DirtTTCAsITerrainTileComponent) {
          topColor = Vector4Animation.Color(.4f, .133f, 0, .2f);
          sideColor = Vector4Animation.Color(.266f, .1f, 0, .2f);
        } else if (ttc is MudTTCAsITerrainTileComponent) {
          // topColor = Vector4Animation.Color(.35f, .11f, 0f);
          // sideColor = Vector4Animation.Color(.23f, .08f, 0f);
          
          topColor = Vector4Animation.Color(.20f + 0.03f * terrainTile.elevation, .02f + 0.015f * terrainTile.elevation, 0f);
          sideColor = Vector4Animation.Color(.08f + 0.03f * terrainTile.elevation, 0.015f * terrainTile.elevation, 0f);
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
          var offset = new System.Random(location.groupX + location.groupY + location.indexInGroup + terrainTile.elevation).Next() % 628 / 100.0f;
          topColor =
              //Vector4Animation.Color(.075f, .1f, 0.4f);
              new Vector4Animation(
                  // SineFloatAnimation.Make(.5f, offset * .1f, .06f, .09f),
                  // SineFloatAnimation.Make(.5f, offset * .1f, .08f, .12f),
                  // SineFloatAnimation.Make(.5f, offset * .1f, .03f, .05f),
                  SineFloatAnimation.Make(.8f, offset, .07f, .08f),
                  SineFloatAnimation.Make(.8f, offset, .092f, .108f),
                  SineFloatAnimation.Make(.8f, offset, .36f, .44f),
                  new ConstantFloatAnimation(1.0f));
          outlineColor = Vector4Animation.Color(0f, 0f, 0f);
          sideColor = Vector4Animation.Color(.1f, .15f, 0.5f);

          overlay =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      "o",
                      Vector4Animation.Color(.1f, .15f, 0.5f),
                      0,
                      1,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0f, 0f, 0.5f)),
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
                      1,
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
                      1,
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
                        1,
                        OutlineMode.WithOutline),
                    false,
                    Vector4Animation.Color(0, 0, 0));
            overlayLocked = true;
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
                        1,
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
                        1,
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
            new SymbolDescription("f-3", Vector4Animation.Color(0f, 0f, 0f, .8f), 0, 1, OutlineMode.WithOutline, Vector4Animation.Color(0, 0, 0)),
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
                        1,
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
                      1,
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
                      1,
                      OutlineMode.WithOutline),
                  true,
                  Vector4Animation.Color(1f, 1f, 1f));
          featureLocked = true;
        } else if (ttc is TreeTTCAsITerrainTileComponent) {
          var symbols = new string[] {"tree1", "tree2", "tree3"};
          var symbolI = new System.Random(location.groupX + location.groupY + location.indexInGroup + terrainTile.elevation).Next() % symbols.Length;
          var symbol = symbols[symbolI];
          
          feature =
              new ExtrudedSymbolDescription(
                  RenderPriority.SYMBOL,
                  new SymbolDescription(
                      symbol,
                      Vector4Animation.Color(0, .3f, 0),
                      0,
                      1,
                      OutlineMode.WithOutline),
                  false,
                  Vector4Animation.Color(0f, .2f, 0f));
        } else if (ttc is CaveTTCAsITerrainTileComponent) {
              feature =
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "p",
                          Vector4Animation.Color(0, 0, 0),
                          0,
                          1,
                          OutlineMode.WithOutline,
                          Vector4Animation.Color(1, 1, 1)),
                      false,
                      Vector4Animation.Color(1f, 1f, 1f));
              featureLocked = true;
        } else if (ttc is OnFireTTCAsITerrainTileComponent) {
          feature =
              new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "r-3",
                          Vector4Animation.Color(1.0f, .4f, 0, 1.5f),
                          0,
                          1,
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
                        1,
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
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.Color(.75f, .75f, 0)));
          } else if (item is BlazeRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(1f, 1f, 0, 1.5f),
                        0,
                        1,
                        OutlineMode.NoOutline),
                    true,
                    Vector4Animation.Color(0f, 0f, 0)));
          } else if (item is ExplosionRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        1,
                        OutlineMode.NoOutline),
                    true,
                    Vector4Animation.Color(0f, 0f, 0)));
          } else if (item is SlowRodAsIItem) {
            itemSymbolDescriptionByItemId.Add(
                ttc.id,
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(0f, .5f, 1f, 1.5f),
                        0,
                        1,
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
                        1,
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
                        1,
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
                        1,
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
                        1,
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
                        1,
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
                    1,
                    OutlineMode.WithOutline,
                    Vector4Animation.Color(0, 0, 0)),
                true,
                Vector4Animation.Color(0, 0, 1f, 1f)));
      } else if (effect.newValue is UnitFireBombedEventAsITerrainTileEvent ||
          effect.newValue is UnitBlazedEventAsITerrainTileEvent ||
          effect.newValue is UnitExplosionedEventAsITerrainTileEvent) {
        var patternTile = terrain.pattern.patternTiles[location.indexInGroup];

        prismEndTime =
          tileView.ShowPrism(
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      GetTerrainTileShapeSymbol(patternTile),
                      Vector4Animation.Color(1.0f, 0.3f, 0, 0.4f),
                      0,
                      1,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0, 0, 0, 0.4f)),
                  true,
                  Vector4Animation.Color(1.0f, 0.3f, 0, 0.4f)),
              new ExtrudedSymbolDescription(
                  RenderPriority.RUNE,
                  new SymbolDescription(
                      "r-3",
                      Vector4Animation.Color(1.0f, .3f, 0, 0.5f),
                      0,
                      1,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0, 0, 0)),
                  true,
                  Vector4Animation.Color(0, 0, 0, 0.5f)));
        onAnimation?.Invoke(prismEndTime);
      }
    }

    // This class gets to preview an effect before it officially happens.
    // Its main purpose is to stall the effect until this UnitPresenter is ready.
    private class TileEffectStaller :
        ITerrainTileEffectObserver,
        ITerrainTileEffectVisitor,
        IITerrainTileComponentMutBunchObserver,
        IITerrainTileComponentMutBunchEffectVisitor,
        IGameEffectObserver,
        IGameEffectVisitor {

      EffectBroadcaster stallBroadcaster;
      Game game;
      ITerrainTileComponentMutBunchBroadcaster componentsBroadcaster;
      private TerrainTilePresenter terrainTilePresenter;
      private IEffectStaller staller;

      public TileEffectStaller(
          EffectBroadcaster stallBroadcaster,
          Game game,
          TerrainTilePresenter terrainTilePresenter,
          IEffectStaller staller) {
        this.stallBroadcaster = stallBroadcaster;
        this.game = game;
        this.terrainTilePresenter = terrainTilePresenter;
        this.staller = staller;

        game.AddObserver(stallBroadcaster, this);

        stallBroadcaster.AddTerrainTileObserver(terrainTilePresenter.terrainTile.id, this);

        this.componentsBroadcaster = new ITerrainTileComponentMutBunchBroadcaster(stallBroadcaster, terrainTilePresenter.terrainTile.components);
        componentsBroadcaster.AddObserver(this);
      }

      public void Destroy() {
        componentsBroadcaster.RemoveObserver(this);
        stallBroadcaster.RemoveTerrainTileObserver(terrainTilePresenter.terrainTile.id, this);
        game.AddObserver(stallBroadcaster, this);
      }

      public void OnGameEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
      public void visitGameCreateEffect(GameCreateEffect effect) { }
      public void visitGameDeleteEffect(GameDeleteEffect effect) { }
      public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) { }
      public void visitGameSetLevelEffect(GameSetLevelEffect effect) { }
      public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
      public void visitGameSetActingUnitEffect(GameSetActingUnitEffect effect) { }
      public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect effect) { }
      public void visitGameSetActionNumEffect(GameSetActionNumEffect effect) { }
      public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) { }
      public void visitGameSetHideInputEffect(GameSetHideInputEffect effect) { }
      public void visitGameSetEvventEffect(GameSetEvventEffect effect) {
        if (effect.newValue is WaitForEverythingEventAsIGameEvent) {
          staller(terrainTilePresenter.prismEndTime, "prism");
        }
      }

      public void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect) { }
      public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect) {
        staller(terrainTilePresenter.prismEndTime, "prism");
      }
      public void visitTerrainTileSetEvventEffect(TerrainTileSetEvventEffect effect) {
        if (effect.newValue is WaitForUnitEventAsIUnitEvent) {
          staller(terrainTilePresenter.prismEndTime, "prism");
        } else if (effect.newValue is UnitFireBombedEventAsITerrainTileEvent ||
            effect.newValue is UnitBlazedEventAsITerrainTileEvent ||
            effect.newValue is UnitExplosionedEventAsITerrainTileEvent) {
          staller(terrainTilePresenter.prismEndTime, "prism");
        }
      }
      public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect) { }

      public void OnTerrainTileEffect(ITerrainTileEffect effect) { effect.visitITerrainTileEffect(this); }
      public void OnITerrainTileComponentMutBunchAdd(int id) { }
      public void OnITerrainTileComponentMutBunchRemove(int id) { }

      public void visitITerrainTileComponentMutBunchCreateEffect(ITerrainTileComponentMutBunchCreateEffect effect) { }
      public void visitITerrainTileComponentMutBunchDeleteEffect(ITerrainTileComponentMutBunchDeleteEffect effect) { }
    }
  }
}
