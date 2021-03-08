using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveImpulseStrongMutSetRemoveEffect : IMoveImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MoveImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMoveImpulseStrongMutSetEffect.id => id;
  public void visitIMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
