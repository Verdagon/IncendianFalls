using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampVector3Animation : IVector3Animation {
  long startTimeMs;
  long endTimeMs;
  IVector3Animation inner;

  public ClampVector3Animation(
      long startTimeMs,
      long endTimeMs,
      IVector3Animation inner) {
    this.startTimeMs = startTimeMs;
    this.endTimeMs = endTimeMs;
    this.inner = inner;
  }

  public Vector3 Get(long timeMs) {
    if (timeMs < startTimeMs) {
      return inner.Get(startTimeMs);
    }
    if (timeMs >= endTimeMs) {
      return inner.Get(endTimeMs);
    }
    return inner.Get(timeMs);
  }

  public IVector3Animation Simplify(long timeMs) {
    if (timeMs >= endTimeMs) {
      Vector3 value = inner.Get(endTimeMs);
      if (value.EqualsE(new Vector3(0, 0, 0), .001f)) {
        return new IdentityVector3Animation();
      } else {
        return new ConstantVector3Animation(value);
      }
    }
    return this;
  }
}
