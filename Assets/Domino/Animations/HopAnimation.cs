using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopAnimation : IMatrix4x4Animation {
  float startTime;
  float endTime;
  Vector3 horizontalOffset;
  float height;

  public HopAnimation(
      float startTime,
      float endTime,
      Vector3 horizontalOffset,
      float height) {
    this.startTime = startTime;
    this.endTime = endTime;
    this.horizontalOffset = horizontalOffset;
    this.height = height;
  }

  public Matrix4x4 Get(float time) {
    float timeRatio = (time - startTime) / (endTime - startTime);
    timeRatio = Mathf.Clamp01(timeRatio);
    Vector3 translate = horizontalOffset * timeRatio;

    // y = -4xx + 4x is a curve that goes from (0, 0) to (.5, 1) to (1, 0)
    float heightAddition = height * timeRatio * (-4 * timeRatio + 4);

    translate.y += heightAddition;
    return Matrix4x4.Translate(translate);
  }

  public IMatrix4x4Animation Simplify(float time) {
    if (time < endTime) {
      return this;
    } else {
      return new ConstantMatrix4x4Animation(Matrix4x4.Translate(horizontalOffset));
    }
  }
}
