using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PursueImpulseDeleteEffect : IPursueImpulseEffect {
  public readonly int id;
  public PursueImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IPursueImpulseEffect.id => id;
  public void visitIPursueImpulseEffect(IPursueImpulseEffectVisitor visitor) {
    visitor.visitPursueImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPursueImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
