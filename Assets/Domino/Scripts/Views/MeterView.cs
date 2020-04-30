using System;
using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

namespace Domino {
  public class MeterView : MonoBehaviour {
    private bool initialized = false;

    public static readonly int METER_RENDER_QUEUE = 3000;

    private IClock clock;

    // The main object that lives in world space. It has no rotation or scale,
    // just a translation to the center of the tile the unit is in.
    // public GameObject gameObject; (provided by unity)

    // If we have 10/15 health, then this represents the 10.
    // Lives inside this.gameObject.
    public GameObject filledPart;

    // If we have 10/15 health, then this represents the 5 we're missing.
    // Lives inside this.gameObject.
    public GameObject emptyPart;

    Instantiator instantiator;
    float ratio_;

    public void Init(
      IClock clock,
        Instantiator instantiator,
        IVector4Animation filledColor,
        IVector4Animation emptyColor,
        float ratio) {
      this.clock = clock;
      this.instantiator = instantiator;

      filledPart.GetComponent<ColorChangerThing>().Set(filledColor, RenderPriority.METER);
      emptyPart.GetComponent<ColorChangerThing>().Set(emptyColor, RenderPriority.METER);
      InnerSetRatio(ratio);

      initialized = true;
    }

    public void Destruct() {
      CheckInitialized();
      Destroy(gameObject);
      initialized = false;
    }

    public void CheckInitialized() {
      if (!initialized) {
        throw new System.Exception("SymbolView component not initialized!");
      }
    }

    public float ratio {
      get { CheckInitialized(); return ratio_; }
      set {
        CheckInitialized();
        if (ratio_ != value) {
          InnerSetRatio(value);
        }
      }
    }

    private void InnerSetRatio(float newRatio) {
      ratio_ = newRatio;

      MatrixBuilder filledPartTransform = new MatrixBuilder(Matrix4x4.identity);
      filledPartTransform.Translate(new Vector3(0.5f, 0, 0.5f));
      filledPartTransform.Scale(new Vector3(ratio_, 1, 1));
      filledPartTransform.Translate(new Vector3(0, 0, 0));
      filledPart.transform.FromMatrix(filledPartTransform.matrix);

      MatrixBuilder emptyPartTransform = new MatrixBuilder(Matrix4x4.identity);
      emptyPartTransform.Translate(new Vector3(0.5f, 0, 0.5f));
      emptyPartTransform.Scale(new Vector3(1 - ratio_, 1, 1));
      emptyPartTransform.Translate(new Vector3(ratio_, 0, 0));
      emptyPart.transform.FromMatrix(emptyPartTransform.matrix);
    }

    public void Start() {
      CheckInitialized();
    }

    public void Fade(long durationMs) {
      var filledPartAnimator = ColorChangerThing.MakeOrGetFrom(clock, filledPart);
      filledPartAnimator.Set(
          FadeAnimator.Fade(filledPartAnimator.Get(), clock.GetTimeMs(), durationMs),
          RenderPriority.SYMBOL);
      var emptyPartAnimator = ColorChangerThing.MakeOrGetFrom(clock, emptyPart);
      emptyPartAnimator.Set(
          FadeAnimator.Fade(emptyPartAnimator.Get(), clock.GetTimeMs(), durationMs),
          RenderPriority.SYMBOL);
    }
  }
}