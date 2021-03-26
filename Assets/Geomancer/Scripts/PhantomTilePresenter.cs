﻿using System;
using System.Collections.Generic;
using AthPlayer;
using Geomancer.Model;
using UnityEngine;
using Domino;

namespace Geomancer {
  public class PhantomTilePresenterTile : MonoBehaviour {
    // PhantomTilePresenter attaches this to the TileView it creates, so that when EditorPresenter
    // raycasts, it can know the PhantomTilePresenter that owns this TileView.
    // This approach is an implementation detail of the Editor, and shouldnt enter Domino.
    public PhantomTilePresenter presenter;

    public void Init(PhantomTilePresenter presenter) {
      this.presenter = presenter;
    }
  }

  public class PhantomTilePresenter {
    //public delegate void OnMouseInEvent();
    //public delegate void OnMouseOutEvent();
    public delegate void OnPhantomTileClickedEvent();

    IClock clock;
    ITimer timer;
  Pattern pattern;
    public readonly Location location;
    Instantiator instantiator;

    Vector3 tileCenter;
    TileView tileView;

    public PhantomTilePresenter(
        IClock clock,
      ITimer timer,
        Pattern pattern,
        Location location,
        Instantiator instantiator) {
      this.clock = clock;
      this.timer = timer;
      this.pattern = pattern;
      this.location = location;
      this.instantiator = instantiator;

      var positionVec2 = pattern.GetTileCenter(location);

      tileCenter = new Vec3(positionVec2.x, positionVec2.y, 0).ToUnity();

      ResetViews();
    }

    private (Vector4Animation, Vector4Animation) GetColors(bool highlighted) {
      var frontColor = highlighted ? Vector4Animation.Color(.2f, .2f, .2f) : Vector4Animation.Color(0f, 0, 0f);
      var sideColor = highlighted ? Vector4Animation.Color(.2f, .2f, .2f) : Vector4Animation.Color(0f, 0, 0f);
      return (frontColor, sideColor);
    }
    
    public void SetHighlighted(bool highlighted) {
      var (frontColor, sideColor) = GetColors(highlighted);
      tileView.SetFrontColor(frontColor);
      tileView.SetSidesColor(sideColor);
      // tileView.SetDescription(GetTileDescription(pattern, location, highlighted));
    }

    private void ResetViews() {
      var tileDescription = GetTileDescription(pattern, location, false);

      if (tileView != null) {
        tileView.DestroyTile();
        tileView = null;
      }

      tileView = instantiator.CreateTileView(clock, timer, tileCenter, tileDescription);
      tileView.gameObject.AddComponent<PhantomTilePresenterTile>().Init(this);
      // tileView.SetDescription(tileDescription);
    }

    private static TileDescription GetTileDescription(Pattern pattern, Location location, bool highlighted) {
      var patternTile = pattern.patternTiles[location.indexInGroup];

      var frontColor = highlighted ? Vector4Animation.Color(.1f, .1f, .1f) : Vector4Animation.Color(0f, 0, 0f);
      var sideColor = highlighted ? Vector4Animation.Color(.1f, .1f, .1f) : Vector4Animation.Color(0f, 0, 0f);

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

      return
        new TileDescription(
              .2f,
              patternTile.rotateRadianards / 1000f * 180f / (float)Math.PI,
              1,
              new ExtrudedSymbolDescription(
                RenderPriority.TILE,
                new SymbolDescription(
                    symbolName,
                    frontColor,
                    patternTile.rotateRadianards / 1000f * 180f / (float)Math.PI,
                    1,
                    OutlineMode.WithOutline,
                    Vector4Animation.Color(.2f, .2f, .2f)),
                false,
                sideColor),
              null,
              null,
              new List<(ulong, ExtrudedSymbolDescription)>());
    }

    public void DestroyPhantomTilePresenter() {
      tileView.DestroyTile();
    }

    // public void OnStrMutListEffect(IStrMutListEffect effect) {
    //   effect.visitIStrMutListEffect(this);
    // }
    //
    // public void visitStrMutListCreateEffect(StrMutListCreateEffect effect) { }
    //
    // public void visitStrMutListDeleteEffect(StrMutListDeleteEffect effect) { }
    //
    // public void visitStrMutListAddEffect(StrMutListAddEffect effect) {
    //   ResetViews();
    // }
    //
    // public void visitStrMutListRemoveEffect(StrMutListRemoveEffect effect) {
    //   ResetViews();
    // }
  }
}
