using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveImpulseCreateEffect : IMoveImpulseEffect {
  public readonly int id;
  public MoveImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IMoveImpulseEffect.id => id;
  public void visit(IMoveImpulseEffectVisitor visitor) {
    visitor.visitMoveImpulseCreateEffect(this);
  }
}

}
