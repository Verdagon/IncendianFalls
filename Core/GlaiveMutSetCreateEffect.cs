using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveMutSetCreateEffect : IGlaiveMutSetEffect {
  public readonly int id;
  public GlaiveMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IGlaiveMutSetEffect.id => id;
  public void visit(IGlaiveMutSetEffectVisitor visitor) {
    visitor.visitGlaiveMutSetCreateEffect(this);
  }
}

}
