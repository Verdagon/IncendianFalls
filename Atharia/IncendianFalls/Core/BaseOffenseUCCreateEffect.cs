using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseOffenseUCCreateEffect : IBaseOffenseUCEffect {
  public readonly int id;
  public BaseOffenseUCCreateEffect(int id) {
    this.id = id;
  }
  int IBaseOffenseUCEffect.id => id;
  public void visit(IBaseOffenseUCEffectVisitor visitor) {
    visitor.visitBaseOffenseUCCreateEffect(this);
  }
}

}
