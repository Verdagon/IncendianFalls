using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThenFloatAnimation : IFloatAnimation {
  readonly long firstAnimationEndTimeMs;
  IFloatAnimation firstAnimation;
  IFloatAnimation secondAnimation;

  public ThenFloatAnimation(
      long firstAnimationEndTimeMs,
      IFloatAnimation firstAnimation,
      IFloatAnimation secondAnimation) {
    this.firstAnimationEndTimeMs = firstAnimationEndTimeMs;
    this.firstAnimation = firstAnimation;
    this.secondAnimation = secondAnimation;
  }

  public float Get(long timeMs) {
    // This shouldnt be necessary actually, cuz once we pass
    // firstAnimationEndTime we'll get simplified away.
    if (timeMs < firstAnimationEndTimeMs) {
      return firstAnimation.Get(timeMs);
    } else {
      return secondAnimation.Get(timeMs);
    }
  }

  public IFloatAnimation Simplify(long timeMs) {
    firstAnimation = firstAnimation.Simplify(timeMs);
    if (timeMs < firstAnimationEndTimeMs) {
      return this;
    }
    secondAnimation = secondAnimation.Simplify(timeMs);
    return secondAnimation;
  }
}
