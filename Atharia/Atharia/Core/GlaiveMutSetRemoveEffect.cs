using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveMutSetRemoveEffect : IGlaiveMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GlaiveMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGlaiveMutSetEffect.id => id;
  public void visitIGlaiveMutSetEffect(IGlaiveMutSetEffectVisitor visitor) {
    visitor.visitGlaiveMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
