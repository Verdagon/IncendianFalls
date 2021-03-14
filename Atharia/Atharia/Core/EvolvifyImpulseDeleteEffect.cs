using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvolvifyImpulseDeleteEffect : IEvolvifyImpulseEffect {
  public readonly int id;
  public EvolvifyImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IEvolvifyImpulseEffect.id => id;
  public void visitIEvolvifyImpulseEffect(IEvolvifyImpulseEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
