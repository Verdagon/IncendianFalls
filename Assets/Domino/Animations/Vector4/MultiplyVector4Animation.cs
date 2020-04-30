using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class MultiplyVector4Animation : IVector4Animation {
    IVector4Animation vec;
    float multiplicand;

    public MultiplyVector4Animation(IVector4Animation vec, float multiplicand) {
      this.vec = vec;
      this.multiplicand = multiplicand;
    }

    public Vector4 Get(long timeMs) {
      var innerVector4 = vec.Get(timeMs);
      return innerVector4 * multiplicand;
    }

    public IVector4Animation Simplify(long timeMs) {
      return this;
    }
  }
}