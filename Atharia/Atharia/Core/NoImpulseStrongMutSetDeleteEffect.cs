using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct NoImpulseStrongMutSetDeleteEffect : INoImpulseStrongMutSetEffect {
  public readonly int id;
  public NoImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int INoImpulseStrongMutSetEffect.id => id;
  public void visitINoImpulseStrongMutSetEffect(INoImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitNoImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitNoImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
