using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListDeleteEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public IUnitEventMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visit(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListDeleteEffect(this);
  }
}

}
