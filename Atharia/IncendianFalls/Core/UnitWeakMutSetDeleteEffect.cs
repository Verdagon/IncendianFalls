using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitWeakMutSetDeleteEffect : IUnitWeakMutSetEffect {
  public readonly int id;
  public UnitWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUnitWeakMutSetEffect.id => id;
  public void visit(IUnitWeakMutSetEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetDeleteEffect(this);
  }
}

}
