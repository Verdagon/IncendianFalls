using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireImpulseStrongMutSetRemoveEffect : IFireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FireImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFireImpulseStrongMutSetEffect.id => id;
  public void visit(IFireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitFireImpulseStrongMutSetRemoveEffect(this);
  }
}

}
