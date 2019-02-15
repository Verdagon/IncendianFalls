using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveDirectiveDeleteEffect : IMoveDirectiveEffect {
  public readonly int id;
  public MoveDirectiveDeleteEffect(int id) {
    this.id = id;
  }
  int IMoveDirectiveEffect.id => id;
  public void visit(IMoveDirectiveEffectVisitor visitor) {
    visitor.visitMoveDirectiveDeleteEffect(this);
  }
}
       
}
