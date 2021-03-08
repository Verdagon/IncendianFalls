using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PursueImpulseIncarnation : IPursueImpulseEffectVisitor {
  public readonly int weight;
  public readonly bool isClearPath;
  public PursueImpulseIncarnation(
      int weight,
      bool isClearPath) {
    this.weight = weight;
    this.isClearPath = isClearPath;
  }
  public PursueImpulseIncarnation Copy() {
    return new PursueImpulseIncarnation(
weight,
isClearPath    );
  }

  public void visitPursueImpulseCreateEffect(PursueImpulseCreateEffect e) {}
  public void visitPursueImpulseDeleteEffect(PursueImpulseDeleteEffect e) {}


  public void ApplyEffect(IPursueImpulseEffect effect) { effect.visitIPursueImpulseEffect(this); }
}

}
