using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class Vector4ComponentAnimation : IFloatAnimation {
    IVector4Animation inner;
    int component;

    public Vector4ComponentAnimation(IVector4Animation inner, int component) {
      this.inner = inner;
      this.component = component;
    }

    public float Get(long timeMs) {
      return inner.Get(timeMs)[component];
    }

    public IFloatAnimation Simplify(long timeMs) {
      inner = inner.Simplify(timeMs);
      if (inner is ConstantVector4Animation) {
        return new ConstantFloatAnimation(Get(timeMs));
      }
      return this;
    }
  }
}
