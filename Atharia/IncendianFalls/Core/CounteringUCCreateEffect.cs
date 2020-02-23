using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounteringUCCreateEffect : ICounteringUCEffect {
  public readonly int id;
  public CounteringUCCreateEffect(int id) {
    this.id = id;
  }
  int ICounteringUCEffect.id => id;
  public void visit(ICounteringUCEffectVisitor visitor) {
    visitor.visitCounteringUCCreateEffect(this);
  }
}

}
