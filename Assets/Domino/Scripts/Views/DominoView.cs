using System;
using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

namespace Domino {
  public class DominoDescription {
    public readonly bool large;
    public readonly Color color;
    public DominoDescription(
        bool large,
        Color color) {
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

    // The main object that lives in world space. It has no rotation or scale,
    // just a translation to the center of the tile the unit is in.
    // public GameObject gameObject; (provided by unity)

    // Object with a transform for the mesh, for example for rotating it.
    // Lives inside this.gameObject.
    private GameObject innerObject;

    Instantiator instantiator;

    bool large_;
    Color color_;

    public void Init(
        Instantiator instantiator,
        DominoDescription dominoDescription) {
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

    public bool large {
      get { return large_;  }
      set {
        if (large_ != value) {
          InnerSetLarge(value);
        }
      }
    }
    public Color color {
      get { return color_; }
      set {
        if (!color_.Equals(value)) {
          InnerSetColor(value);
        }
      }
    }

    private void InnerSetLarge(bool newLarge) {
      Destroy(innerObject);
      if (newLarge) {
        innerObject = instantiator.CreateLargeDomino();
      } else {
        innerObject = instantiator.CreateSmallDomino();
      }
      large_ = newLarge;
      InnerSetColor(color_);
    }

    private void InnerSetColor(Color newColor) {
      innerObject.GetComponent<ColorChanger>().SetColor(newColor, RenderPriority.DOMINO);
      color_ = newColor;
    }

    public void Start() {
      if (!initialized) {
        throw new System.Exception("SymbolView component not initialized!");
      }
    }

    public void Fade(float duration) {
      GetOrCreateOpacityAnimator().opacityAnimation =
          new ClampFloatAnimation(Time.time, Time.time + duration,
              new LinearFloatAnimation(Time.time, 1.0f, -1.0f / duration));
    }

    private OpacityAnimator GetOrCreateOpacityAnimator() {
      var animator = innerObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = innerObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.renderPriority = RenderPriority.DOMINO;
      }
      return animator;
    }
  }
}