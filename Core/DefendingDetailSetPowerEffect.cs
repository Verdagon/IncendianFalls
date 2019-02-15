using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DefendingDetailSetPowerEffect : IDefendingDetailEffect {
  public readonly int id;
  public readonly int newValue;
  public DefendingDetailSetPowerEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IDefendingDetailEffect.id => id;

  public void visit(IDefendingDetailEffectVisitor visitor) {
    visitor.visitDefendingDetailSetPowerEffect(this);
  }
}
           
}
