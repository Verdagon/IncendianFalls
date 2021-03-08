using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveImpulseDeleteEffect : IMoveImpulseEffect {
  public readonly int id;
  public MoveImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IMoveImpulseEffect.id => id;
  public void visitIMoveImpulseEffect(IMoveImpulseEffectVisitor visitor) {
    visitor.visitMoveImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMoveImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
