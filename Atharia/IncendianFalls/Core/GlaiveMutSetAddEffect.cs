using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveMutSetAddEffect : IGlaiveMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GlaiveMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGlaiveMutSetEffect.id => id;
  public void visitIGlaiveMutSetEffect(IGlaiveMutSetEffectVisitor visitor) {
    visitor.visitGlaiveMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveMutSetEffect(this);
  }
}

}
