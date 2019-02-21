using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveMutSetCreateEffect : IGlaiveMutSetEffect {
  public readonly int id;
  public readonly GlaiveMutSetIncarnation incarnation;
  public GlaiveMutSetCreateEffect(
      int id,
      GlaiveMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IGlaiveMutSetEffect.id => id;
  public void visit(IGlaiveMutSetEffectVisitor visitor) {
    visitor.visitGlaiveMutSetCreateEffect(this);
  }
}

}
