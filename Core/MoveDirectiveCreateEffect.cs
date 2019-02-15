using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveDirectiveCreateEffect : IMoveDirectiveEffect {
  public readonly int id;
  public readonly MoveDirectiveIncarnation incarnation;
  public MoveDirectiveCreateEffect(
      int id,
      MoveDirectiveIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMoveDirectiveEffect.id => id;
  public void visit(IMoveDirectiveEffectVisitor visitor) {
    visitor.visitMoveDirectiveCreateEffect(this);
  }
}
       
}
