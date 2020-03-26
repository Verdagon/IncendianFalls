using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PursueImpulseCreateEffect : IPursueImpulseEffect {
  public readonly int id;
  public readonly PursueImpulseIncarnation incarnation;
  public PursueImpulseCreateEffect(int id, PursueImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IPursueImpulseEffect.id => id;
  public void visitIPursueImpulseEffect(IPursueImpulseEffectVisitor visitor) {
    visitor.visitPursueImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPursueImpulseEffect(this);
  }
}

}
