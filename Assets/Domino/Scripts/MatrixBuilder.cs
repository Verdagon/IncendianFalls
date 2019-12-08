using System;

using UnityEngine;

public struct MatrixBuilder {
  public Matrix4x4 matrix;

  public MatrixBuilder(Matrix4x4 matrix) {
    this.matrix = matrix;
  }
  public void Translate(Vector3 translate) {
    matrix = Matrix4x4.Translate(translate) * matrix;
  }
  public void Scale(Vector3 scale) {
    matrix = Matrix4x4.Scale(scale) * matrix;
  }
  public void Rotate(Quaternion rotation) {
    matrix = Matrix4x4.Rotate(rotation) * matrix;
  }
}
