using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct NoImpulseCreateEffect : INoImpulseEffect {
  public readonly int id;
  public readonly NoImpulseIncarnation incarnation;
  public NoImpulseCreateEffect(int id, NoImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int INoImpulseEffect.id => id;
  public void visitINoImpulseEffect(INoImpulseEffectVisitor visitor) {
    visitor.visitNoImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitNoImpulseEffect(this);
  }
}

}
