using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireImpulseStrongMutSetDeleteEffect : IFireImpulseStrongMutSetEffect {
  public readonly int id;
  public FireImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFireImpulseStrongMutSetEffect.id => id;
  public void visitIFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
