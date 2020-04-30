using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFloatAnimation : IFloatAnimation {
  IFloatAnimation a;
  IFloatAnimation b;

  public AddFloatAnimation(IFloatAnimation a, IFloatAnimation b) {
    this.a = a;
    this.b = b;
  }

  public float Get(long timeMs) {
    return a.Get(timeMs) + b.Get(timeMs);
  }

  public IFloatAnimation Simplify(long timeMs) {
    if (a is ConstantFloatAnimation || a is IdentityFloatAnimation) {
      return b;
    }
    if (b is ConstantFloatAnimation || b is IdentityFloatAnimation) {
      return a;
    }
    return this;
  }
}
