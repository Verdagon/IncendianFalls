using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungeAnimation : IMatrix4x4Animation {
  readonly long startTimeMs;
  readonly long endTimeMs;
  readonly Vector3 lungeOffset; // If Vec3(2, 0, 0) that means we'll lunge 2 to the right.

  public LungeAnimation(
      long startTimeMs,
      long endTimeMs,
      Vector3 lungeOffset) {
    this.startTimeMs = startTimeMs;
    this.endTimeMs = endTimeMs;
    this.lungeOffset = lungeOffset;
  }

  public Matrix4x4 Get(long timeMs) {
    float timeRatio = (float)(timeMs - startTimeMs) / (endTimeMs - startTimeMs);
    timeRatio = Mathf.Clamp01(timeRatio);

    // lungePositionRatio should go sharply from (0, 0) to (.5, 1) to (1, 0)
    float lungePositionRatio;
    if (timeRatio < .5f) {
      lungePositionRatio = timeRatio * 2;
    } else {
      lungePositionRatio = -2 * timeRatio + 2;
    }

    Vector3 translate = lungeOffset * lungePositionRatio;

    return Matrix4x4.Translate(translate);
  }

  public Matrix4x4 GetEndValue() {
    return Matrix4x4.identity;
  }

  public bool IsDone(long timeMs) {
    return timeMs >= endTimeMs;
  }

  public IMatrix4x4Animation Simplify(long timeMs) {
    if (timeMs < endTimeMs) {
      return this;
    } else {
      return new IdentityMatrix4x4Animation();
    }
  }
}
