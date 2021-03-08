using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CommMutListDeleteEffect : ICommMutListEffect {
  public readonly int id;
  public CommMutListDeleteEffect(int id) {
    this.id = id;
  }
  int ICommMutListEffect.id => id;
  public void visitICommMutListEffect(ICommMutListEffectVisitor visitor) {
    visitor.visitCommMutListDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCommMutListEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
