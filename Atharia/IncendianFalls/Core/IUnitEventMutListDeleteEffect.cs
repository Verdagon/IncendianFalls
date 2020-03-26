using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListDeleteEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public IUnitEventMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visitIIUnitEventMutListEffect(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIUnitEventMutListEffect(this);
  }
}

}
