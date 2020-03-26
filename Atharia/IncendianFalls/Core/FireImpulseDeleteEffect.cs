using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireImpulseDeleteEffect : IFireImpulseEffect {
  public readonly int id;
  public FireImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IFireImpulseEffect.id => id;
  public void visitIFireImpulseEffect(IFireImpulseEffectVisitor visitor) {
    visitor.visitFireImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireImpulseEffect(this);
  }
}

}
