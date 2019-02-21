using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveMutSetDeleteEffect : IGlaiveMutSetEffect {
  public readonly int id;
  public GlaiveMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IGlaiveMutSetEffect.id => id;
  public void visit(IGlaiveMutSetEffectVisitor visitor) {
    visitor.visitGlaiveMutSetDeleteEffect(this);
  }
}

}
