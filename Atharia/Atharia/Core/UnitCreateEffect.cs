using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitCreateEffect : IUnitEffect {
  public readonly int id;
  public readonly UnitIncarnation incarnation;
  public UnitCreateEffect(int id, UnitIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUnitEffect.id => id;
  public void visitIUnitEffect(IUnitEffectVisitor visitor) {
    visitor.visitUnitCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
