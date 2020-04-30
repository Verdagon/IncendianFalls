using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class AddVector4Animation : IVector4Animation {
    IVector4Animation a;
    IVector4Animation b;

    public AddVector4Animation(IVector4Animation a, IVector4Animation b) {
      this.a = a;
      this.b = b;
    }

    public Vector4 Get(long timeMs) {
      return b.Get(timeMs) + a.Get(timeMs);
    }

    public IVector4Animation Simplify(long timeMs) {
      a = a.Simplify(timeMs);
      b = b.Simplify(timeMs);
      if (a is IdentityVector4Animation) {
        return b;
      }
      if (b is IdentityVector4Animation) {
        return a;
      }
      if ((a is ConstantVector4Animation) && (b is ConstantVector4Animation)) {
        Vector4 constant = Get(timeMs);
        if (constant.EqualsE(new Vector4(0, 0, 0), .001f)) {
          return new IdentityVector4Animation();
        } else {
          return new ConstantVector4Animation(constant);
        }
      }
      return this;
    }
  }
}