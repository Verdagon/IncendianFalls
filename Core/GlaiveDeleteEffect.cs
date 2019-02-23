using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GlaiveDeleteEffect : IGlaiveEffect {
  public readonly int id;
  public GlaiveDeleteEffect(int id) {
    this.id = id;
  }
  int IGlaiveEffect.id => id;
  public void visit(IGlaiveEffectVisitor visitor) {
    visitor.visitGlaiveDeleteEffect(this);
  }
}
       
}
