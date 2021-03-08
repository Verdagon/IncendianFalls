using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneImpulseCreateEffect : ITemporaryCloneImpulseEffect {
  public readonly int id;
  public readonly TemporaryCloneImpulseIncarnation incarnation;
  public TemporaryCloneImpulseCreateEffect(int id, TemporaryCloneImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITemporaryCloneImpulseEffect.id => id;
  public void visitITemporaryCloneImpulseEffect(ITemporaryCloneImpulseEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
