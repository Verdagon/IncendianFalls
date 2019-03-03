using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveDirectiveUCDeleteEffect : IMoveDirectiveUCEffect {
  public readonly int id;
  public MoveDirectiveUCDeleteEffect(int id) {
    this.id = id;
  }
  int IMoveDirectiveUCEffect.id => id;
  public void visit(IMoveDirectiveUCEffectVisitor visitor) {
    visitor.visitMoveDirectiveUCDeleteEffect(this);
  }
}

}
