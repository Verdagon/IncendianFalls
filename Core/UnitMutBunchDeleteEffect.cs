using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitMutBunchDeleteEffect : IUnitMutBunchEffect {
  public readonly int id;
  public UnitMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IUnitMutBunchEffect.id => id;
  public void visit(IUnitMutBunchEffectVisitor visitor) {
    visitor.visitUnitMutBunchDeleteEffect(this);
  }
}

}
