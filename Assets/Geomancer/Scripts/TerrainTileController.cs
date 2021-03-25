using System;
using System.Collections.Generic;
using AthPlayer;
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

  public class TerrainTilePresenter {
    IClock clock;
    ITimer timer;
    
    MemberToViewMapper vivimap;
    public readonly Location location;
    TerrainTile terrainTile;
    Instantiator instantiator;
    private Geomancer.Model.Terrain terrain;

    TileView tileView;
    UnitView unitView;

    private ulong nextMemberId = 1;
    // (member ID, member string)
    private List<(ulong, string)> members = new List<(ulong, string)>();
    
    // (member ID, value)
    private List<(ulong, IVector4Animation)> membersFrontColors = new List<(ulong, IVector4Animation)>();
    
    // (member ID, value)
    private List<(ulong, IVector4Animation)> membersSideColors = new List<(ulong, IVector4Animation)>();

    private bool highlightedZork;

    private bool selectedZork;

    public TerrainTilePresenter(
      IClock clock,
      ITimer timer,
        MemberToViewMapper vivimap,
        Geomancer.Model.Terrain terrain,
        Location location,
        TerrainTile terrainTile,
        Instantiator instantiator) {
      this.clock = clock;
      this.timer = timer;
      this.vivimap = vivimap;
      this.location = location;
      this.terrainTile = terrainTile;
      this.instantiator = instantiator;
      this.terrain = terrain;

      // terrainTile.members.AddObserver(broadcaster, this);
      // terrainTile.AddObserver(broadcaster, this);

      var eternalMemberId = nextMemberId++;
      membersFrontColors.Add((eternalMemberId, Vector4Animation.Color(.4f, .4f, 0, 1)));
      membersSideColors.Add((eternalMemberId, Vector4Animation.Color(.4f, .4f, 0, 1)));

      foreach (var member in terrainTile.members) {
        OnAddMember(member);
      }

      var patternTile = terrain.pattern.patternTiles[location.indexInGroup];
      var pattern = terrain.pattern;
      string symbolName = "a";
      switch (pattern.name) {
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

      var initialTileDescription =
          new TileDescription(
              terrain.elevationStepHeight * ModelExtensions.ModelToUnityMultiplier, // elevation
              patternTile.rotateRadianards / 1000f * 180f / (float)Math.PI,
              terrainTile.elevation, // depth
              new ExtrudedSymbolDescription(
                  RenderPriority.TILE,
                  new SymbolDescription(
                      symbolName, // symbol name
                      CalculateTintedFrontColor(membersFrontColors[membersFrontColors.Count - 1].Item2, selectedZork, highlightedZork),
                      patternTile.rotateRadianards / 1000f * 180f / (float)Math.PI,
                      1, // scale
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0, 0, 0)), // outline
                  true,
                  membersSideColors[membersSideColors.Count - 1].Item2),
              null,
              null,
              new SortedDictionary<int, ExtrudedSymbolDescription>());

      var position = CalculatePosition(terrain.elevationStepHeight, terrain.pattern, location, terrainTile.elevation);
      tileView = instantiator.CreateTileView(clock, timer, position, initialTileDescription);
      tileView.gameObject.AddComponent<TerrainTilePresenterTile>().Init(this);
    }

    private static Vector3 CalculatePosition(int elevationStepHeight, Pattern pattern, Location location, int elevation) {
      var positionVec2 = pattern.GetTileCenter(location);
      var positionVec3 = new Vec3(positionVec2.x, positionVec2.y, elevation * elevationStepHeight);
      return positionVec3.ToUnity();
    }

    public void SetHighlighted(bool highlightedZork) {
      this.highlightedZork = highlightedZork;
      RefreshFrontColor();
    }
    public void SetSelected(bool selectedZork) {
      this.selectedZork = selectedZork;
      RefreshFrontColor();
    }

    private void RefreshFrontColor() {
      tileView.SetFrontColor(
          CalculateTintedFrontColor(
              membersFrontColors[membersFrontColors.Count - 1].Item2, selectedZork, highlightedZork));
    }

    private void RefreshSideColor() {
      tileView.SetSidesColor(membersSideColors[membersSideColors.Count - 1].Item2);
    }

    private void RefreshPosition() {
      var position = CalculatePosition(terrain.elevationStepHeight, terrain.pattern, location, terrainTile.elevation);
      tileView.gameObject.transform.localPosition = position;
    }

    private void RefreshDepth() {
      tileView.SetDepth(terrainTile.elevation);
    }

    private void OnAddMember(string member) {
      ulong memberId = nextMemberId++;
      members.Add((memberId, member));
      var visitor = new AttributeAddingVisitor(this, memberId);
      foreach (var thing in vivimap.getEntries(member)) {
        // Will set membersFrontColors/dirty, membersSideColors/dirty, etc.
        thing.visit(visitor);
      }
    }

    public void OnRemoveMember(int index) {
      var (memberId, member) = members[index];
      members.RemoveAt(index);
      var visitor = new AttributeRemovingVisitor(this, memberId);
      foreach (var thing in vivimap.getEntries(member)) {
        thing.visit(visitor);
      }
    }

    public void AddMember(string member) {
      terrainTile.members.Add(member);
      OnAddMember(member);
      RefreshFrontColor();
      RefreshSideColor();
    }

    public void RemoveMember(int index) {
      terrainTile.members.RemoveAt(index);
      OnRemoveMember(index);
      RefreshFrontColor();
      RefreshSideColor();
    }

    public void SetElevation(int elevation) {
      terrainTile.elevation = elevation;
      RefreshPosition();
      RefreshDepth();
    }

    // private void ResetViews() {
    //   var (tileDescription, maybeUnitDescription) = GetDescriptions();
    //
    //   if (tileView != null) {
    //     tileView.DestroyTile();
    //     tileView = null;
    //   }
    //
    //   var positionVec2 = terrain.pattern.GetTileCenter(location);
    //   tileCenter =
    //     new UnityEngine.Vector3(
    //               positionVec2.x,
    //               terrainTile.elevation * terrain.elevationStepHeight,
    //               positionVec2.y);
    //
    //   tileView = instantiator.CreateTileView(clock, timer, tileCenter, tileDescription);
    //   tileView.gameObject.AddComponent<TerrainTilePresenterTile>().Init(this);
    //   // tileView.SetDescription(tileDescription);
    //
    //   if (unitView) {
    //     unitView.Destruct();
    //     unitView = null;
    //   }
    //
    //   if (maybeUnitDescription != null) {
    //     unitView = instantiator.CreateUnitView(clock, null, tileCenter, maybeUnitDescription, new Vector3(0, -8, 16));
    //     // unitView.SetDescription(maybeUnitDescription);
    //   }
    // }

    // private (TileDescription, UnitDescription) GetDescriptions() {
    //   int lowestNeighborElevation = int.MaxValue;
    //   foreach (var adjacentLocation in terrain.GetAdjacentExistingLocations(location, false)) {
    //     var adjacentTerrainTile = terrain.tiles[adjacentLocation];
    //     lowestNeighborElevation = Math.Min(lowestNeighborElevation, adjacentTerrainTile.elevation);
    //   }
    //   int neededDepth = Math.Max(1, terrainTile.elevation - lowestNeighborElevation);
    //
    //   var patternTile = terrain.pattern.patternTiles[location.indexInGroup];
    //
    //   string symbolName = "a";
    //   switch (terrain.pattern.name) {
    //     case "square":
    //       if (patternTile.shapeIndex == 0) {
    //         symbolName = "six";
    //       }
    //       break;
    //     case "pentagon9":
    //       if (patternTile.shapeIndex == 0) {
    //         symbolName = "i";
    //       } else if (patternTile.shapeIndex == 1) {
    //         symbolName = "h";
    //       }
    //       break;
    //     case "hex":
    //       if (patternTile.shapeIndex == 0) {
    //         symbolName = "five";
    //       }
    //       break;
    //   }
    //
    //   var defaultUnitDescription =
    //     new UnitDescription(
    //       null,
    //       new DominoDescription(false, Vector4Animation.Color(.5f, 0, .5f)),
    //       new ExtrudedSymbolDescription(
    //         RenderPriority.DOMINO,
    //         new SymbolDescription(
    //           "a", Vector4Animation.Color(0, 1, 0), 45, 1, OutlineMode.WithBackOutline),
    //         true,
    //         Vector4Animation.Color(0, 0, 0)),
    //       new List<KeyValuePair<int, ExtrudedSymbolDescription>>(),
    //       1,
    //       1);
    //
    //   var members = new List<String>();
    //   foreach (var member in this.terrainTile.members) {
    //     members.Add(member);
    //   }
    //   var (tileDescription, unitDescription) =
    //     vivimap.Vivify(defaultTileDescription, defaultUnitDescription, members);
    //   if (highlighted || selected) {
    //     IVector4Animation frontColor = CalculateFrontColor(selected, highlighted);
    //     tileDescription =
    //       tileDescription.WithTileSymbolDescription(
    //         tileDescription.tileSymbolDescription.WithSymbol(
    //           tileDescription.tileSymbolDescription.symbol.WithFrontColor(
    //             frontColor)));
    //   }
    //   return (tileDescription, unitDescription);
    // }

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

    // public void OnStrMutListEffect(IStrMutListEffect effect) {
    //   effect.visitIStrMutListEffect(this);
    // }

    // public void visitStrMutListCreateEffect(StrMutListCreateEffect effect) {}
    //
    // public void visitStrMutListDeleteEffect(StrMutListDeleteEffect effect) {}
    //
    // public void visitStrMutListAddEffect(StrMutListAddEffect effect) {
    //   Update(updater => {
    //     updater.AddMember(effect.element);
    //   });
    // }
    //
    // public void visitStrMutListRemoveEffect(StrMutListRemoveEffect effect) {
    //   Update(updater => {
    //     updater.RemoveMember(effect.index);
    //   });
    // }
    //
    // public void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect) {}
    //
    // public void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect) {
    //   tileView.DestroyTile();
    // }
    //
    // public void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect) {
    //   this.Update(updater => {
    //     updater.SetElevation(effect.newValue);
    //   });
    // }
    //
    // public void OnTerrainTileEffect(ITerrainTileEffect effect) {
    //   effect.visitITerrainTileEffect(this);
    // }
    
    private static IVector4Animation CalculateTintedFrontColor(
        IVector4Animation membersFrontColor, bool selected, bool highlighted) {
      if (selected && highlighted) {
        return
            new MultiplyVector4Animation(
                new AddVector4Animation(
                    new MultiplyVector4Animation(membersFrontColor, 5f),
                    new MultiplyVector4Animation(Vector4Animation.Color(1, 1, 1, 1), 3f)),
                1 / 8f);
      } else if (selected) {
        return
            new MultiplyVector4Animation(
                new AddVector4Animation(
                    new MultiplyVector4Animation(membersFrontColor, 6f),
                    new MultiplyVector4Animation(Vector4Animation.Color(1, 1, 1, 1), 2f)),
                1 / 8f);
      } else if (highlighted) {
        return
            new MultiplyVector4Animation(
                new AddVector4Animation(
                    new MultiplyVector4Animation(membersFrontColor, 7f),
                    new MultiplyVector4Animation(Vector4Animation.Color(1, 1, 1, 1), 1f)),
                1 / 8f);
      } else {
        return membersFrontColor;
      }
    }
    
    class AttributeAddingVisitor : MemberToViewMapper.IDescriptionVisitor {
      private TerrainTilePresenter presenter;
      private ulong id;
      public AttributeAddingVisitor(TerrainTilePresenter presenter, ulong id) {
        this.presenter = presenter;
        this.id = id;
      }
      public void visitTileTopColor(MemberToViewMapper.TopColorDescriptionForIDescription color) {
        presenter.membersFrontColors.Add((id, color.color));
      }
      public void visitTileSideColor(MemberToViewMapper.SideColorDescriptionForIDescription color) {
        presenter.membersSideColors.Add((id, color.color));
      }
      public void visitTileOverlay(MemberToViewMapper.OverlayDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitTileFeature(MemberToViewMapper.FeatureDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitUnitDomino(MemberToViewMapper.DominoDescriptionForIDescription domino) {
        throw new NotImplementedException();
      }
      public void visitUnitFace(MemberToViewMapper.FaceDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitUnitDetail(MemberToViewMapper.DetailDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitTileItem(MemberToViewMapper.ItemDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
    }
    
    class AttributeRemovingVisitor : MemberToViewMapper.IDescriptionVisitor {
      private TerrainTilePresenter presenter;
      private ulong id;
      public AttributeRemovingVisitor(TerrainTilePresenter presenter, ulong id) {
        this.presenter = presenter;
        this.id = id;
      }
      public void visitTileTopColor(MemberToViewMapper.TopColorDescriptionForIDescription color) {
        presenter.membersFrontColors.RemoveAll(x => x.Item1 == id);
      }
      public void visitTileSideColor(MemberToViewMapper.SideColorDescriptionForIDescription color) {
        presenter.membersSideColors.RemoveAll(x => x.Item1 == id);
      }
      public void visitTileOverlay(MemberToViewMapper.OverlayDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitTileFeature(MemberToViewMapper.FeatureDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitUnitDomino(MemberToViewMapper.DominoDescriptionForIDescription domino) {
        throw new NotImplementedException();
      }
      public void visitUnitFace(MemberToViewMapper.FaceDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitUnitDetail(MemberToViewMapper.DetailDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
      public void visitTileItem(MemberToViewMapper.ItemDescriptionForIDescription symbol) {
        throw new NotImplementedException();
      }
    }
  }
}
