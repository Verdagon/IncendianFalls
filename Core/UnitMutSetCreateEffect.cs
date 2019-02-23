using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitMutSetCreateEffect : IUnitMutSetEffect {
  public readonly int id;
  public readonly UnitMutSetIncarnation incarnation;
  public UnitMutSetCreateEffect(
      int id,
      UnitMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUnitMutSetEffect.id => id;
  public void visit(IUnitMutSetEffectVisitor visitor) {
    visitor.visitUnitMutSetCreateEffect(this);
  }
}

}
