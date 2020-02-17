using System;
using System.Collections.Generic;
using IncendianFalls;
using Geomancer.Model;
using UnityEngine;
using Domino;

namespace Geomancer {
  public class PhantomTilePresenter : IButts,
      IStrMutListEffectObserver, IStrMutListEffectVisitor {
    public delegate void OnMouseInEvent();
    public delegate void OnMouseOutEvent();
    public delegate void OnMouseClickEvent();

    Pattern pattern;
    Location location;
    Instantiator instantiator;

    Vector3 tileCenter;
    TileView tileView;

    public event OnMouseInEvent mouseIn;
    public event OnMouseOutEvent mouseOut;
    public event OnMouseClickEvent mouseClick;

    public PhantomTilePresenter(
        Pattern pattern,
        Location location,
        Instantiator instantiator) {
      this.pattern = pattern;
      this.location = location;
      this.instantiator = instantiator;

      var positionVec2 = pattern.GetTileCenter(location);

      tileCenter = new UnityEngine.Vector3(positionVec2.x, 0, positionVec2.y);

      ResetViews();
    }

    private void ResetViews() {
      var tileDescription = GetTileDescription(pattern, location);

      if (tileView != null) {
        tileView.DestroyTile();
        tileView = null;
      }

      tileView = instantiator.CreateTileView(tileCenter, tileDescription);
      tileView.SetDescription(tileDescription);
      tileView.observers.Add(this);
    }

    private static TileDescription GetTileDescription(Pattern pattern, Location location) {
      var patternTile = pattern.patternTiles[location.indexInGroup];
     return
          new TileDescription(
              1,
              patternTile.rotateDegrees,
              1,
              new ExtrudedSymbolDescription(
                RenderPriority.TILE,
                new SymbolDescription(
                    ((char)('0' + patternTile.shapeIndex)).ToString(),
                    new Color(0f, 0, 0f),
                    patternTile.rotateDegrees,
                    OutlineMode.WithOutline,
                    new Color(.2f, .2f, .2f)),
                false,
                new Color(0f, 0f, 0)),
              null,
              null,
              new SortedDictionary<int, ExtrudedSymbolDescription>());
    }

    public void DestroyPhantomTilePresenter() {
      tileView.DestroyTile();
    }

    public void OnMouseClick() {
      mouseClick.Invoke();
    }

    public void OnMouseIn() {
      mouseIn.Invoke();
    }

    public void OnMouseOut() {
      mouseOut.Invoke();
    }

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
