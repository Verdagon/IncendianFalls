using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitCreateEffect : IUnitEffect {
  public readonly int id;
  public readonly UnitIncarnation incarnation;
  public UnitCreateEffect(
      int id,
      UnitIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUnitEffect.id => id;
  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitCreateEffect(this);
  }
}
       
}
