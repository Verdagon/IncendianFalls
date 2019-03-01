using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveDirectiveUCCreateEffect : IMoveDirectiveUCEffect {
  public readonly int id;
  public MoveDirectiveUCCreateEffect(int id) {
    this.id = id;
  }
  int IMoveDirectiveUCEffect.id => id;
  public void visit(IMoveDirectiveUCEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCCreateEffect(this);
  }
}

}
