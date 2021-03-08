using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitMutSetDeleteEffect : IUnitMutSetEffect {
  public readonly int id;
  public UnitMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUnitMutSetEffect.id => id;
  public void visitIUnitMutSetEffect(IUnitMutSetEffectVisitor visitor) {
    visitor.visitUnitMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
