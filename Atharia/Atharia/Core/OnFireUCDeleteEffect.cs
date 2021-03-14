using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct OnFireUCDeleteEffect : IOnFireUCEffect {
  public readonly int id;
  public OnFireUCDeleteEffect(int id) {
    this.id = id;
  }
  int IOnFireUCEffect.id => id;
  public void visitIOnFireUCEffect(IOnFireUCEffectVisitor visitor) {
    visitor.visitOnFireUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitOnFireUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
