using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveStrongMutSetDeleteEffect : IGlaiveStrongMutSetEffect {
  public readonly int id;
  public GlaiveStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IGlaiveStrongMutSetEffect.id => id;
  public void visit(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetDeleteEffect(this);
  }
}

}