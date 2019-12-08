using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearFloatAnimation : IFloatAnimation {
  float startTime;
  float valueAtStart;
  float slope;

  public LinearFloatAnimation(float startTime, float valueAtStart, float slope) {
    this.startTime = startTime;
    this.valueAtStart = valueAtStart;
    this.slope = slope;
  }

  public float Get(float time) {
    return valueAtStart + (time - startTime) * slope;
  }

  public IFloatAnimation Simplify(float time) {
    return this;
  }
}
