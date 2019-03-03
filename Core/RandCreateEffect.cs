using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RandCreateEffect : IRandEffect {
  public readonly int id;
  public RandCreateEffect(int id) {
    this.id = id;
  }
  int IRandEffect.id => id;
  public void visit(IRandEffectVisitor visitor) {
    visitor.visitRandCreateEffect(this);
  }
}

}
