using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveImpulseStrongMutSetCreateEffect : IMoveImpulseStrongMutSetEffect {
  public readonly int id;
  public MoveImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMoveImpulseStrongMutSetEffect.id => id;
  public void visit(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetCreateEffect(this);
  }
}

}
