using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampMatrix4x4Animation : IMatrix4x4Animation {
  long startTimeMs;
  long endTimeMs;
  IMatrix4x4Animation inner;

  public ClampMatrix4x4Animation(
      long startTimeMs,
      long endTimeMs,
      IMatrix4x4Animation inner) {
    this.startTimeMs = startTimeMs;
    this.endTimeMs = endTimeMs;
    this.inner = inner;
  }

  public Matrix4x4 Get(long timeMs) {
    if (timeMs < startTimeMs) {
      return inner.Get(startTimeMs);
    }
    if (timeMs >= endTimeMs) {
      return inner.Get(endTimeMs);
    }
    return inner.Get(timeMs);
  }

  public IMatrix4x4Animation Simplify(long time) {
    if (time >= endTimeMs) {
      Matrix4x4 value = inner.Get(endTimeMs);
      if (value.EqualsE(Matrix4x4.identity, .001f)) {
        return new IdentityMatrix4x4Animation();
      } else {
        return new ConstantMatrix4x4Animation(value);
      }
    }
    return this;
  }
}
