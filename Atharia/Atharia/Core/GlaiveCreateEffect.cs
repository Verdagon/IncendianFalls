using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GlaiveCreateEffect : IGlaiveEffect {
  public readonly int id;
  public readonly GlaiveIncarnation incarnation;
  public GlaiveCreateEffect(int id, GlaiveIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IGlaiveEffect.id => id;
  public void visitIGlaiveEffect(IGlaiveEffectVisitor visitor) {
    visitor.visitGlaiveCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
