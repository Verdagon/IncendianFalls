using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvaporateImpulseCreateEffect : IEvaporateImpulseEffect {
  public readonly int id;
  public readonly EvaporateImpulseIncarnation incarnation;
  public EvaporateImpulseCreateEffect(int id, EvaporateImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IEvaporateImpulseEffect.id => id;
  public void visitIEvaporateImpulseEffect(IEvaporateImpulseEffectVisitor visitor) {
    visitor.visitEvaporateImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvaporateImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
