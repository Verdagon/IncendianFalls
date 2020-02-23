using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearFloatAnimation : IFloatAnimation {
  long startTimeMs;
  float valueAtStart;
  float slope;

  public LinearFloatAnimation(long startTimeMs, float valueAtStart, float slope) {
    this.startTimeMs = startTimeMs;
    this.valueAtStart = valueAtStart;
    this.slope = slope;
  }

  public float Get(long timeMs) {
    return valueAtStart + (timeMs - startTimeMs) * slope;
  }

  public IFloatAnimation Simplify(long timeMs) {
    return this;
  }
}
