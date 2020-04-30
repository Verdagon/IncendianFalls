using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class ConstantVector4Animation : IVector4Animation {
    Vector4 matrix;

    public ConstantVector4Animation(Vector4 matrix) {
      this.matrix = matrix;
    }

    public Vector4 Get(long timeMs) {
      return matrix;
    }

    public IVector4Animation Simplify(long timeMs) {
      return this;
    }
  }
}