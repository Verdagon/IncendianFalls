using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyImpulseCreateEffect : IDefyImpulseEffect {
  public readonly int id;
  public readonly DefyImpulseIncarnation incarnation;
  public DefyImpulseCreateEffect(int id, DefyImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDefyImpulseEffect.id => id;
  public void visitIDefyImpulseEffect(IDefyImpulseEffectVisitor visitor) {
    visitor.visitDefyImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
