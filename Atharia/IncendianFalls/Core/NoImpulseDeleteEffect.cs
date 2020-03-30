using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct NoImpulseDeleteEffect : INoImpulseEffect {
  public readonly int id;
  public NoImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int INoImpulseEffect.id => id;
  public void visitINoImpulseEffect(INoImpulseEffectVisitor visitor) {
    visitor.visitNoImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitNoImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
