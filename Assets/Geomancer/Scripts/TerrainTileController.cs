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
    private List<(ulong, IVector4Animation)> membersSideColors = new List<(ulong, IVector4Animation)>();
    private List<(ulong, ExtrudedSymbolDescription)> membersFeatures = new List<(ulong, ExtrudedSymbolDescription)>();
    private List<(ulong, ExtrudedSymbolDescription)> membersOverlays = new List<(ulong, ExtrudedSymbolDescription)>();
    private List<(ulong, ExtrudedSymbolDescription)> membersItems = new List<(ulong, ExtrudedSymbolDescription)>();
    private List<(ulong, DominoDescription)> membersDominos = new List<(ulong, DominoDescription)>();
    private List<(ulong, ExtrudedSymbolDescription)> membersUnitFaces = new List<(ulong, ExtrudedSymbolDescription)>();
    private List<(ulong, ExtrudedSymbolDescription)> membersDetails = new List<(ulong, ExtrudedSymbolDescription)>();

    private bool highlighted;
    private bool selected;

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
                      CalculateTintedFrontColor(membersFrontColors[membersFrontColors.Count - 1].Item2, selected, highlighted),
                      patternTile.rotateRadianards / 1000f * 180f / (float)Math.PI,
                      1, // scale
                      OutlineMode.WithOutline,
                      Vector4Animation.Color(0, 0, 0)), // outline
                  true,
                  membersSideColors[membersSideColors.Count - 1].Item2),
              CalculateMaybeOverlay(membersOverlays),
              CalculateMaybeFeature(membersFeatures),
              membersItems);

      var position = CalculatePosition(terrain.elevationStepHeight, terrain.pattern, location, terrainTile.elevation);
      tileView = instantiator.CreateTileView(clock, timer, position, initialTileDescription);
      tileView.gameObject.AddComponent<TerrainTilePresenterTile>().Init(this);
    }

    private static Vector3 CalculatePosition(int elevationStepHeight, Pattern pattern, Location location, int elevation) {
      var positionVec2 = pattern.GetTileCenter(location);
      var positionVec3 = new Vec3(positionVec2.x, positionVec2.y, elevation * elevationStepHeight);
      return positionVec3.ToUnity();
    }

    public void SetHighlighted(bool highlighted) {
      this.highlighted = highlighted;
      RefreshFrontColor();
    }
    public void SetSelected(bool selected) {
      this.selected = selected;
      RefreshFrontColor();
    }

    private void RefreshFrontColor() {
      tileView.SetFrontColor(
          CalculateTintedFrontColor(
              membersFrontColors[membersFrontColors.Count - 1].Item2, selected, highlighted));
    }

    private void RefreshSideColor() {
      tileView.SetSidesColor(membersSideColors[membersSideColors.Count - 1].Item2);
    }
    
    private void RefreshFeature() {
      tileView.SetFeature(membersFeatures.Count == 0 ? null : membersFeatures[membersFeatures.Count - 1].Item2);
    }
    
    private void RefreshUnit() {
      if (this.unitView != null) {
        this.unitView.Destruct();
        this.unitView = null;
      }
      if (this.unitView == null && membersDominos.Count > 0 && membersUnitFaces.Count > 0) {
        var position = CalculatePosition(terrain.elevationStepHeight, terrain.pattern, location, terrainTile.elevation);
        var unitDescription =
            new UnitDescription(
                null,
                membersDominos[membersDominos.Count - 1].Item2,
                membersUnitFaces[membersUnitFaces.Count - 1].Item2,
                membersDetails,
                1,
                1);
        this.unitView =
            instantiator.CreateUnitView(clock, null, position, unitDescription, new Vector3(0, -8, 16));
      }
    }

    private void RefreshDomino() {
      // TODO: replace this with a call to unitView.SetDomino
      RefreshUnit();
    }

    private void RefreshUnitFace() {
      // TODO: replace this with a call to unitView.SetFace
      RefreshUnit();
    }

    private void RefreshOverlay() {
      tileView.SetOverlay(membersOverlays.Count == 0 ? null : membersOverlays[membersOverlays.Count - 1].Item2);
    }

    private void RefreshPosition() {
      var position = CalculatePosition(terrain.elevationStepHeight, terrain.pattern, location, terrainTile.elevation);
      tileView.gameObject.transform.localPosition = position;
    }

    private void RefreshDepth() {
      tileView.SetDepth(terrainTile.elevation);
    }

    private void RefreshItems() {
      tileView.ClearItems();
      foreach (var x in membersItems) {
        tileView.AddItem(x.Item1, x.Item2);
      }
    }

    private void RefreshDetails() {
      // unitView.ClearDetails();
      // foreach (var x in membersDetails) {
      //   unitView.AddItem(x.Item1, x.Item2);
      // }
      // TODO put the above in
      RefreshUnit();
    }

    private void OnAddMember(string member) {
      ulong memberId = nextMemberId++;
      members.Add((memberId, member));
      // var visitor = new AttributeAddingVisitor(this, memberId);
      foreach (var thing in vivimap.getEntries(member)) {
        if (thing is MemberToViewMapper.TopColorDescriptionForIDescription topColor) {
          membersFrontColors.Add((memberId, topColor.color));
          if (tileView != null) {
            RefreshFrontColor();
          }
        } else if (thing is MemberToViewMapper.SideColorDescriptionForIDescription sideColor) {
          membersSideColors.Add((memberId, sideColor.color));
          if (tileView != null) {
            RefreshSideColor();
          }
        } else if (thing is MemberToViewMapper.OverlayDescriptionForIDescription overlay) {
          membersOverlays.Add((memberId, overlay.symbol));
          if (tileView != null) {
            RefreshOverlay();
          }
        } else if (thing is MemberToViewMapper.FeatureDescriptionForIDescription feature) {
          membersFeatures.Add((memberId, feature.symbol));
          if (tileView != null) {
            RefreshFeature();
          }
        } else if (thing is MemberToViewMapper.DominoDescriptionForIDescription domino) {
          membersDominos.Add((memberId, domino.domino));
          if (unitView != null) {
            RefreshUnit();
          } else {
            RefreshDomino();
          }
        } else if (thing is MemberToViewMapper.FaceDescriptionForIDescription face) {
          membersUnitFaces.Add((memberId, face.symbol));
          if (unitView == null) {
            RefreshUnit();
          } else {
            RefreshUnitFace();
          }
        } else if (thing is MemberToViewMapper.DetailDescriptionForIDescription detail) {
          membersDetails.Add((memberId, detail.symbol));
          if (unitView == null) {
            RefreshUnit();
          } else {
            RefreshDetails();
          }
        } else if (thing is MemberToViewMapper.ItemDescriptionForIDescription item) {
          membersItems.Add((memberId, item.symbol));
          if (tileView != null) {
            RefreshItems();
          }
        } else {
          Asserts.Assert(false);
        }
      }
    }

    public void OnRemoveMember(int index) {
      var (memberId, member) = members[index];
      members.RemoveAt(index);
      foreach (var thing in vivimap.getEntries(member)) {
        if (thing is MemberToViewMapper.TopColorDescriptionForIDescription topColor) {
          membersFrontColors.RemoveAll(x => x.Item1 == memberId);
          if (tileView != null) {
            RefreshFrontColor();
          }
        } else if (thing is MemberToViewMapper.SideColorDescriptionForIDescription sideColor) {
          membersSideColors.RemoveAll(x => x.Item1 == memberId);
          if (tileView != null) {
            RefreshSideColor();
          }
        } else if (thing is MemberToViewMapper.OverlayDescriptionForIDescription overlay) {
          membersOverlays.RemoveAll(x => x.Item1 == memberId);
          if (tileView != null) {
            RefreshOverlay();
          }
        } else if (thing is MemberToViewMapper.FeatureDescriptionForIDescription feature) {
          membersFeatures.RemoveAll(x => x.Item1 == memberId);
          if (tileView != null) {
            RefreshFeature();
          }
        } else if (thing is MemberToViewMapper.DominoDescriptionForIDescription domino) {
          membersDominos.RemoveAll(x => x.Item1 == memberId);
          if (unitView != null) {
            RefreshDomino();
          }
        } else if (thing is MemberToViewMapper.FaceDescriptionForIDescription face) {
          membersUnitFaces.RemoveAll(x => x.Item1 == memberId);
          if (unitView != null) {
            RefreshUnitFace();
          }
        } else if (thing is MemberToViewMapper.DetailDescriptionForIDescription detail) {
          membersDetails.RemoveAll(x => x.Item1 == memberId);
          if (unitView != null) {
            RefreshDetails();
          }
        } else if (thing is MemberToViewMapper.ItemDescriptionForIDescription item) {
          membersItems.RemoveAll(x => x.Item1 == memberId);
          if (tileView != null) {
            RefreshItems();
          }
        } else {
          Asserts.Assert(false);
        }
      }
    }

    public void AddMember(string member) {
      terrainTile.members.Add(member);
      OnAddMember(member);
    }

    public void RemoveMember(string member) {
      int index = terrainTile.members.IndexOf(member);
      Asserts.Assert(index >= 0);
      terrainTile.members.RemoveAt(index);
      OnRemoveMember(index);
    }

    public void RemoveMemberAt(int index) {
      terrainTile.members.RemoveAt(index);
      OnRemoveMember(index);
    }

    public void SetElevation(int elevation) {
      terrainTile.elevation = elevation;
      RefreshPosition();
      RefreshDepth();
    }

    //   if (unitView) {
    //     unitView.Destruct();
    //     unitView = null;
    //   }
    //
    //   if (maybeUnitDescription != null) {
    //     
    //     // unitView.SetDescription(maybeUnitDescription);
    //   }
    // }

    // private (TileDescription, UnitDescription) GetDescriptions() {
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
    //       new List<(int, ExtrudedSymbolDescription)>(),
    //       1,
    //       1);
    //
    //   var (tileDescription, unitDescription) =
    //     vivimap.Vivify(defaultTileDescription, defaultUnitDescription, members);
    //   return (tileDescription, unitDescription);
    // }

    public void DestroyTerrainTilePresenter() {
      tileView.DestroyTile();
    }

    private static ExtrudedSymbolDescription CalculateMaybeOverlay(List<(ulong, ExtrudedSymbolDescription)> membersOverlays) {
      return membersOverlays.Count == 0 ? null : membersOverlays[membersOverlays.Count - 1].Item2;
    }

    private static ExtrudedSymbolDescription CalculateMaybeFeature(List<(ulong, ExtrudedSymbolDescription)> membersFeatures) {
      return membersFeatures.Count == 0 ? null : membersFeatures[membersFeatures.Count - 1].Item2;
    }

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
  }
}
