using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvaporateImpulseDeleteEffect : IEvaporateImpulseEffect {
  public readonly int id;
  public EvaporateImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IEvaporateImpulseEffect.id => id;
  public void visitIEvaporateImpulseEffect(IEvaporateImpulseEffectVisitor visitor) {
    visitor.visitEvaporateImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvaporateImpulseEffect(this);
  }
}

}
