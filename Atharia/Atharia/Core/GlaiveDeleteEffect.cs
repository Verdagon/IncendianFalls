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
  public void visitIGlaiveEffect(IGlaiveEffectVisitor visitor) {
    visitor.visitGlaiveDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
