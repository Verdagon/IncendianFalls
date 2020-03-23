using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVector3Animation : IVector3Animation {
  IVector3Animation a;
  IVector3Animation b;

  public AddVector3Animation(IVector3Animation a, IVector3Animation b) {
    this.a = a;
    this.b = b;
  }

  public Vector3 Get(long timeMs) {
    return b.Get(timeMs) + a.Get(timeMs);
  }

  public IVector3Animation Simplify(long timeMs) {
    a = a.Simplify(timeMs);
    b = b.Simplify(timeMs);
    if (a is IdentityVector3Animation) {
      return b;
    }
    if (b is IdentityVector3Animation) {
      return a;
    }
    if ((a is ConstantVector3Animation) && (b is ConstantVector3Animation)) {
      Vector3 constant = Get(timeMs);
      if (constant.EqualsE(new Vector3(0, 0, 0), .001f)) {
        return new IdentityVector3Animation();
      } else {
        return new ConstantVector3Animation(constant);
      }
    }
    return this;
  }
}
