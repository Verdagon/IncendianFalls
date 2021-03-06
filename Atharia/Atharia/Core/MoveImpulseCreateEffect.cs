using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MoveImpulseCreateEffect : IMoveImpulseEffect {
  public readonly int id;
  public readonly MoveImpulseIncarnation incarnation;
  public MoveImpulseCreateEffect(int id, MoveImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMoveImpulseEffect.id => id;
  public void visitIMoveImpulseEffect(IMoveImpulseEffectVisitor visitor) {
    visitor.visitMoveImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMoveImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
