using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveMutSetRemoveEffect : IGlaiveMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GlaiveMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGlaiveMutSetEffect.id => id;
  public void visit(IGlaiveMutSetEffectVisitor visitor) {
    visitor.visitGlaiveMutSetRemoveEffect(this);
  }
}

}
