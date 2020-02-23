using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMatrix4x4Animation : IMatrix4x4Animation {
  long startTimeMs;
  long endTimeMs;
  Matrix4x4 valueAtStart;
  Matrix4x4 valueAtEnd;

  public LinearMatrix4x4Animation(long startTimeMs, long endTimeMs, Matrix4x4 valueAtStart, Matrix4x4 valueAtEnd) {
    this.startTimeMs = startTimeMs;
    this.endTimeMs = endTimeMs;
    this.valueAtStart = valueAtStart;
    this.valueAtEnd = valueAtEnd;
  }

  public Matrix4x4 Get(long timeMs) {
    float ratio = (float)(timeMs - startTimeMs) / (endTimeMs - startTimeMs);

    var startPosition = valueAtStart.ExtractPosition();
    var startRotation = valueAtStart.ExtractRotation();
    var startScale = valueAtStart.ExtractScale();

    var endPosition = valueAtEnd.ExtractPosition();
    var endRotation = valueAtEnd.ExtractRotation();
    var endScale = valueAtEnd.ExtractScale();

    var rotation = Quaternion.Slerp(startRotation, endRotation, ratio);
    var position = startPosition * (1 - ratio) + endPosition * ratio;
    var scale = startScale * (1 - ratio) + endScale * ratio;

    Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);

    return matrix;
  }

  public IMatrix4x4Animation Simplify(long timeMs) {
    return this;
  }
}
