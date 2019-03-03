using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitDeleteEffect : IUnitEffect {
  public readonly int id;
  public UnitDeleteEffect(int id) {
    this.id = id;
  }
  int IUnitEffect.id => id;
  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitDeleteEffect(this);
  }
}

}
