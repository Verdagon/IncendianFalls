using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MoveDirectiveUCMutSetDeleteEffect : IMoveDirectiveUCMutSetEffect {
  public readonly int id;
  public MoveDirectiveUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMoveDirectiveUCMutSetEffect.id => id;
  public void visit(IMoveDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCMutSetDeleteEffect(this);
  }
}

}
