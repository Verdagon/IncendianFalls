using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveDirectiveUCMutSetCreateEffect : IMoveDirectiveUCMutSetEffect {
  public readonly int id;
  public readonly MoveDirectiveUCMutSetIncarnation incarnation;
  public MoveDirectiveUCMutSetCreateEffect(
      int id,
      MoveDirectiveUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMoveDirectiveUCMutSetEffect.id => id;
  public void visit(IMoveDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCMutSetCreateEffect(this);
  }
}

}
