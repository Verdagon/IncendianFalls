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
  public void visitIGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
