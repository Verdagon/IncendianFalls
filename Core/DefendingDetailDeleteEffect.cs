using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DefendingDetailDeleteEffect : IDefendingDetailEffect {
  public readonly int id;
  public DefendingDetailDeleteEffect(int id) {
    this.id = id;
  }
  int IDefendingDetailEffect.id => id;
  public void visit(IDefendingDetailEffectVisitor visitor) {
    visitor.visitDefendingDetailDeleteEffect(this);
  }
}
       
}
