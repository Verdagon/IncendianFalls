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
  public void visitIUnitWeakMutSetEffect(IUnitWeakMutSetEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetEffect(this);
  }
}

}
