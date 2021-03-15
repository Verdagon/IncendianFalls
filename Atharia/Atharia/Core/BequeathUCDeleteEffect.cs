using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BequeathUCDeleteEffect : IBequeathUCEffect {
  public readonly int id;
  public BequeathUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBequeathUCEffect.id => id;
  public void visitIBequeathUCEffect(IBequeathUCEffectVisitor visitor) {
    visitor.visitBequeathUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBequeathUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
