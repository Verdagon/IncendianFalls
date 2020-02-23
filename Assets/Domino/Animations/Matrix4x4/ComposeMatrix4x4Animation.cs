using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposeMatrix4x4Animation : IMatrix4x4Animation {
  IMatrix4x4Animation a;
  IMatrix4x4Animation b;

  public ComposeMatrix4x4Animation(IMatrix4x4Animation a, IMatrix4x4Animation b) {
    this.a = a;
    this.b = b;
  }

  public Matrix4x4 Get(long timeMs) {
    return b.Get(timeMs) * a.Get(timeMs);
  }

  public IMatrix4x4Animation Simplify(long timeMs) {
    a = a.Simplify(timeMs);
    b = b.Simplify(timeMs);
    if (a is IdentityMatrix4x4Animation) {
      return b;
    }
    if (b is IdentityMatrix4x4Animation) {
      return a;
    }
    if ((a is ConstantMatrix4x4Animation) && (b is ConstantMatrix4x4Animation)) {
      Matrix4x4 constant = Get(timeMs);
      if (constant.EqualsE(Matrix4x4.identity, .001f)) {
        return new IdentityMatrix4x4Animation();
      } else {
        return new ConstantMatrix4x4Animation(constant);
      }
    }
    return this;
  }
}
