using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVector3Animation : IVector3Animation {
  Vector3 matrix;

  public ConstantVector3Animation(Vector3 matrix) {
    this.matrix = matrix;
  }

  public Vector3 Get(long timeMs) {
    return matrix;
  }

  public IVector3Animation Simplify(long timeMs) {
    return this;
  }
}
