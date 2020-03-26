using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseOffenseUCCreateEffect : IBaseOffenseUCEffect {
  public readonly int id;
  public readonly BaseOffenseUCIncarnation incarnation;
  public BaseOffenseUCCreateEffect(int id, BaseOffenseUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBaseOffenseUCEffect.id => id;
  public void visitIBaseOffenseUCEffect(IBaseOffenseUCEffectVisitor visitor) {
    visitor.visitBaseOffenseUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseOffenseUCEffect(this);
  }
}

}
