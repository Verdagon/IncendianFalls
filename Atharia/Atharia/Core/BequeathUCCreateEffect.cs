using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BequeathUCCreateEffect : IBequeathUCEffect {
  public readonly int id;
  public readonly BequeathUCIncarnation incarnation;
  public BequeathUCCreateEffect(int id, BequeathUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBequeathUCEffect.id => id;
  public void visitIBequeathUCEffect(IBequeathUCEffectVisitor visitor) {
    visitor.visitBequeathUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBequeathUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
