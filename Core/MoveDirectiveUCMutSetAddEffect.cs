using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveDirectiveUCMutSetAddEffect : IMoveDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MoveDirectiveUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMoveDirectiveUCMutSetEffect.id => id;
  public void visit(IMoveDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCMutSetAddEffect(this);
  }
}

}
