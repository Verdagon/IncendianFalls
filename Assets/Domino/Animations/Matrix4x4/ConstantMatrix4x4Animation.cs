using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMatrix4x4Animation : IMatrix4x4Animation {
  Matrix4x4 matrix;

  public ConstantMatrix4x4Animation(Matrix4x4 matrix) {
    this.matrix = matrix;
  }

  public Matrix4x4 Get(float time) {
    return matrix;
  }

  public IMatrix4x4Animation Simplify(float time) {
    return this;
  }
}
