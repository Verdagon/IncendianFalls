using System;
using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

namespace Domino {
  public class DominoDescription {
    public readonly bool large;
    public readonly Vector4Animation color;
    public DominoDescription(
        bool large,
        Vector4Animation color) {
      this.large = large;
      this.color = color;
    }

    public override bool Equals(object obj) {
      if (!(obj is DominoDescription)) {
        return false;
      }
      DominoDescription that = obj as DominoDescription;
      return large == that.large && color.Equals(that.color);
    }
    public override int GetHashCode() {
      return (large ? 137 : 0) + 1343 * color.GetHashCode();
    }
  }

  public class DominoView : MonoBehaviour {
    public static readonly int DOMINO_RENDER_QUEUE = 3002;

    private bool initialized = false;

    private IClock clock;

    // The main object that lives in world space. It has no rotation or scale,
    // just a translation to the center of the tile the unit is in.
    // public GameObject gameObject; (provided by unity)

    // Object with a transform for the mesh, for example for rotating it.
    // Lives inside this.gameObject.
    private GameObject innerObject;

    Instantiator instantiator;

    bool large;
    IVector4Animation color;

    public void Init(
        IClock clock,
        Instantiator instantiator,
        DominoDescription dominoDescription) {
      this.clock = clock;
      this.instantiator = instantiator;

      InnerSetLarge(dominoDescription.large);
      InnerSetColor(dominoDescription.color);
      innerObject.transform.SetParent(gameObject.transform, false);

      initialized = true;
    }

    public void SetDescription(DominoDescription newDominoDescription) {
      large = newDominoDescription.large;
      color = newDominoDescription.color;
    }

    private void InnerSetLarge(bool newLarge) {
      Destroy(innerObject);
      if (newLarge) {
        innerObject = instantiator.CreateLargeDomino();
      } else {
        innerObject = instantiator.CreateSmallDomino();
      }
      large = newLarge;
      //InnerSetColor(color);
    }

    private void InnerSetColor(IVector4Animation newColor) {
      ColorChangerThing.MakeOrGetFrom(clock, innerObject).Set(newColor, RenderPriority.DOMINO);
      color = newColor;
    }

    public void Start() {
      if (!initialized) {
        throw new System.Exception("SymbolView component not initialized!");
      }
    }

    public void Fade(long durationMs) {
      var animator = ColorChangerThing.MakeOrGetFrom(clock, innerObject);
      animator.Set(
        FadeAnimator.Fade(animator.Get(), clock.GetTimeMs(), durationMs),
        RenderPriority.DOMINO);
    }
  }
}