using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DefendingDetailCreateEffect : IDefendingDetailEffect {
  public readonly int id;
  public readonly DefendingDetailIncarnation incarnation;
  public DefendingDetailCreateEffect(
      int id,
      DefendingDetailIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDefendingDetailEffect.id => id;
  public void visit(IDefendingDetailEffectVisitor visitor) {
    visitor.visitDefendingDetailCreateEffect(this);
  }
}
       
}
