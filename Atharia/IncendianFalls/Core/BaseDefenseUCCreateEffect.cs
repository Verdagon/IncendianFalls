using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseDefenseUCCreateEffect : IBaseDefenseUCEffect {
  public readonly int id;
  public BaseDefenseUCCreateEffect(int id) {
    this.id = id;
  }
  int IBaseDefenseUCEffect.id => id;
  public void visit(IBaseDefenseUCEffectVisitor visitor) {
    visitor.visitBaseDefenseUCCreateEffect(this);
  }
}

}
