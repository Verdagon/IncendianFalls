using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct StrMutListDeleteEffect : IStrMutListEffect {
  public readonly int id;
  public StrMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IStrMutListEffect.id => id;
  public void visitIStrMutListEffect(IStrMutListEffectVisitor visitor) {
    visitor.visitStrMutListDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStrMutListEffect(this);
  }
}

}
