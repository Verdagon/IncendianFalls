using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideImpulseDeleteEffect : IBideImpulseEffect {
  public readonly int id;
  public BideImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IBideImpulseEffect.id => id;
  public void visit(IBideImpulseEffectVisitor visitor) {
    visitor.visitBideImpulseDeleteEffect(this);
  }
}
       
}
