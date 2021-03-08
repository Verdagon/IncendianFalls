using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveImpulseStrongMutSetAddEffect : IMoveImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MoveImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMoveImpulseStrongMutSetEffect.id => id;
  public void visitIMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
