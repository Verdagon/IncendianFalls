using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CommDeleteEffect : ICommEffect {
  public readonly int id;
  public CommDeleteEffect(int id) {
    this.id = id;
  }
  int ICommEffect.id => id;
  public void visitICommEffect(ICommEffectVisitor visitor) {
    visitor.visitCommDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCommEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
