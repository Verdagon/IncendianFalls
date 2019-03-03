using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveDirectiveUCMutSetRemoveEffect : IMoveDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MoveDirectiveUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMoveDirectiveUCMutSetEffect.id => id;
  public void visit(IMoveDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCMutSetRemoveEffect(this);
  }
}

}
