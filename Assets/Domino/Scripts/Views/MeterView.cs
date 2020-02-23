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
        Color filledColor,
        Color emptyColor,
        float ratio) {
      this.clock = clock;
      this.instantiator = instantiator;

      filledPart.GetComponent<ColorChanger>().SetColor(filledColor, RenderPriority.METER);
      emptyPart.GetComponent<ColorChanger>().SetColor(emptyColor, RenderPriority.METER);
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
      GetOrCreateFilledPartOpacityAnimator().opacityAnimation =
          new ClampFloatAnimation(clock.GetTimeMs(), clock.GetTimeMs() + durationMs,
              new LinearFloatAnimation(clock.GetTimeMs(), 1.0f, -1.0f / durationMs));
      GetOrCreateEmptyPartOpacityAnimator().opacityAnimation =
          new ClampFloatAnimation(clock.GetTimeMs(), clock.GetTimeMs() + durationMs,
              new LinearFloatAnimation(clock.GetTimeMs(), 1.0f, -1.0f / durationMs));
    }

    private OpacityAnimator GetOrCreateFilledPartOpacityAnimator() {
      CheckInitialized();
      var animator = filledPart.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = filledPart.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.Init(clock, RenderPriority.SYMBOL);
      }
      return animator;
    }

    private OpacityAnimator GetOrCreateEmptyPartOpacityAnimator() {
      CheckInitialized();
      var animator = emptyPart.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = emptyPart.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.Init(clock, RenderPriority.SYMBOL);
      }
      return animator;
    }

  }
}