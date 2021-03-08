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
  public void visitIMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
