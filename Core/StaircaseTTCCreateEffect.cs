using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StaircaseTTCCreateEffect : IStaircaseTTCEffect {
  public readonly int id;
  public StaircaseTTCCreateEffect(int id) {
    this.id = id;
  }
  int IStaircaseTTCEffect.id => id;
  public void visit(IStaircaseTTCEffectVisitor visitor) {
    visitor.visitStaircaseTTCCreateEffect(this);
  }
}

}
