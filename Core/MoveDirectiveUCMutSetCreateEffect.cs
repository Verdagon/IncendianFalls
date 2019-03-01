using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveDirectiveUCMutSetCreateEffect : IMoveDirectiveUCMutSetEffect {
  public readonly int id;
  public MoveDirectiveUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMoveDirectiveUCMutSetEffect.id => id;
  public void visit(IMoveDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCMutSetCreateEffect(this);
  }
}

}
