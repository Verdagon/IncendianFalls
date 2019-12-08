using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampMatrix4x4Animation : IMatrix4x4Animation {
  float startTime;
  float endTime;
  IMatrix4x4Animation inner;

  public ClampMatrix4x4Animation(
      float startTime,
      float endTime,
      IMatrix4x4Animation inner) {
    this.startTime = startTime;
    this.endTime = endTime;
    this.inner = inner;
  }

  public Matrix4x4 Get(float time) {
    if (time < startTime) {
      return inner.Get(startTime);
    }
    if (time >= endTime) {
      return inner.Get(endTime);
    }
    return inner.Get(time);
  }

  public IMatrix4x4Animation Simplify(float time) {
    if (time >= endTime) {
      Matrix4x4 value = inner.Get(endTime);
      if (value.EqualsE(Matrix4x4.identity, .001f)) {
        return new IdentityMatrix4x4Animation();
      } else {
        return new ConstantMatrix4x4Animation(value);
      }
    }
    return this;
  }
}
