using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyImpulseStrongMutSetDeleteEffect : IEvolvifyImpulseStrongMutSetEffect {
  public readonly int id;
  public EvolvifyImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IEvolvifyImpulseStrongMutSetEffect.id => id;
  public void visitIEvolvifyImpulseStrongMutSetEffect(IEvolvifyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
