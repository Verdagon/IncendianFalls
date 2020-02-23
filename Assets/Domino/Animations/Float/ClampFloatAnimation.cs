using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampFloatAnimation : IFloatAnimation {
  long startTimeMs;
  long endTimeMs;
  IFloatAnimation inner;

  public ClampFloatAnimation(
      long startTimeMs,
      long endTimeMs,
      IFloatAnimation inner) {
    this.startTimeMs = startTimeMs;
    this.endTimeMs = endTimeMs;
    this.inner = inner;
  }

  public float Get(long timeMs) {
    if (timeMs < startTimeMs) {
      return inner.Get(startTimeMs);
    }
    if (timeMs >= endTimeMs) {
      return inner.Get(endTimeMs);
    }
    return inner.Get(timeMs);
  }

  public IFloatAnimation Simplify(long timeMs) {
    if (timeMs >= endTimeMs) {
      float value = inner.Get(endTimeMs);
      if (Math.Abs(value) < 0.001f) {
        return new IdentityFloatAnimation();
      } else {
        return new ConstantFloatAnimation(value);
      }
    }
    return this;
  }
}
