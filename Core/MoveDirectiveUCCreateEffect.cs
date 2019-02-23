using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveDirectiveUCCreateEffect : IMoveDirectiveUCEffect {
  public readonly int id;
  public readonly MoveDirectiveUCIncarnation incarnation;
  public MoveDirectiveUCCreateEffect(
      int id,
      MoveDirectiveUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMoveDirectiveUCEffect.id => id;
  public void visit(IMoveDirectiveUCEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCCreateEffect(this);
  }
}
       
}
