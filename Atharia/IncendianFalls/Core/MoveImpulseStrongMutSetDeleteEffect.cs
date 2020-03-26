using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveImpulseStrongMutSetDeleteEffect : IMoveImpulseStrongMutSetEffect {
  public readonly int id;
  public MoveImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMoveImpulseStrongMutSetEffect.id => id;
  public void visitIMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMoveImpulseStrongMutSetEffect(this);
  }
}

}
