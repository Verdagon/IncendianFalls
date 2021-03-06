using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvaporateImpulseStrongMutSetDeleteEffect : IEvaporateImpulseStrongMutSetEffect {
  public readonly int id;
  public EvaporateImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IEvaporateImpulseStrongMutSetEffect.id => id;
  public void visitIEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
