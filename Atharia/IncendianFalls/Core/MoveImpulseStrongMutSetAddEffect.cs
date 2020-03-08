using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveImpulseStrongMutSetAddEffect : IMoveImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MoveImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMoveImpulseStrongMutSetEffect.id => id;
  public void visit(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetAddEffect(this);
  }
}

}
