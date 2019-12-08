using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampFloatAnimation : IFloatAnimation {
  float startTime;
  float endTime;
  IFloatAnimation inner;

  public ClampFloatAnimation(
      float startTime,
      float endTime,
      IFloatAnimation inner) {
    this.startTime = startTime;
    this.endTime = endTime;
    this.inner = inner;
  }

  public float Get(float time) {
    if (time < startTime) {
      return inner.Get(startTime);
    }
    if (time >= endTime) {
      return inner.Get(endTime);
    }
    return inner.Get(time);
  }

  public IFloatAnimation Simplify(float time) {
    if (time >= endTime) {
      float value = inner.Get(endTime);
      if (Math.Abs(value) < 0.001f) {
        return new IdentityFloatAnimation();
      } else {
        return new ConstantFloatAnimation(value);
      }
    }
    return this;
  }
}
