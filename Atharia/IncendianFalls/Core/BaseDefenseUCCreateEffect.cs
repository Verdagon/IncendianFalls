using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseDefenseUCCreateEffect : IBaseDefenseUCEffect {
  public readonly int id;
  public readonly BaseDefenseUCIncarnation incarnation;
  public BaseDefenseUCCreateEffect(int id, BaseDefenseUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBaseDefenseUCEffect.id => id;
  public void visitIBaseDefenseUCEffect(IBaseDefenseUCEffectVisitor visitor) {
    visitor.visitBaseDefenseUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseDefenseUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
