using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MiredUCCreateEffect : IMiredUCEffect {
  public readonly int id;
  public MiredUCCreateEffect(int id) {
    this.id = id;
  }
  int IMiredUCEffect.id => id;
  public void visit(IMiredUCEffectVisitor visitor) {
    visitor.visitMiredUCCreateEffect(this);
  }
}

}
