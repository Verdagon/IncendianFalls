using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearVector3Animation : IVector3Animation {
  long startTimeMs;
  long endTimeMs;
  Vector3 valueAtStart;
  Vector3 valueAtEnd;

  public LinearVector3Animation(long startTimeMs, long endTimeMs, Vector3 valueAtStart, Vector3 valueAtEnd) {
    this.startTimeMs = startTimeMs;
    this.endTimeMs = endTimeMs;
    this.valueAtStart = valueAtStart;
    this.valueAtEnd = valueAtEnd;
  }

  public Vector3 Get(long timeMs) {
    float ratio = (float)(timeMs - startTimeMs) / (endTimeMs - startTimeMs);
    return valueAtStart * (1 - ratio) + valueAtEnd * ratio;
  }

  public IVector3Animation Simplify(long timeMs) {
    return this;
  }
}
