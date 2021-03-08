using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyImpulseDeleteEffect : IDefyImpulseEffect {
  public readonly int id;
  public DefyImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyImpulseEffect.id => id;
  public void visitIDefyImpulseEffect(IDefyImpulseEffectVisitor visitor) {
    visitor.visitDefyImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDefyImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
