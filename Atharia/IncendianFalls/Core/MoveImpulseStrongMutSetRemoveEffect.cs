using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveImpulseStrongMutSetRemoveEffect : IMoveImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MoveImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMoveImpulseStrongMutSetEffect.id => id;
  public void visit(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetRemoveEffect(this);
  }
}

}
