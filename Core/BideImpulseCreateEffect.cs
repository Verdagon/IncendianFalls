using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideImpulseCreateEffect : IBideImpulseEffect {
  public readonly int id;
  public readonly BideImpulseIncarnation incarnation;
  public BideImpulseCreateEffect(
      int id,
      BideImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBideImpulseEffect.id => id;
  public void visit(IBideImpulseEffectVisitor visitor) {
    visitor.visitBideImpulseCreateEffect(this);
  }
}
       
}
