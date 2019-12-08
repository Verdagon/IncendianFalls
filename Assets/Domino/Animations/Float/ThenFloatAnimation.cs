using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThenFloatAnimation : IFloatAnimation {
  readonly float firstAnimationEndTime;
  IFloatAnimation firstAnimation;
  IFloatAnimation secondAnimation;

  public ThenFloatAnimation(
      float firstAnimationEndTime,
      IFloatAnimation firstAnimation,
      IFloatAnimation secondAnimation) {
    this.firstAnimationEndTime = firstAnimationEndTime;
    this.firstAnimation = firstAnimation;
    this.secondAnimation = secondAnimation;
  }

  public float Get(float time) {
    // This shouldnt be necessary actually, cuz once we pass
    // firstAnimationEndTime we'll get simplified away.
    if (time < firstAnimationEndTime) {
      return firstAnimation.Get(time);
    } else {
      return secondAnimation.Get(time);
    }
  }

  public IFloatAnimation Simplify(float time) {
    firstAnimation = firstAnimation.Simplify(time);
    if (time < firstAnimationEndTime) {
      return this;
    }
    secondAnimation = secondAnimation.Simplify(time);
    return secondAnimation;
  }
}
