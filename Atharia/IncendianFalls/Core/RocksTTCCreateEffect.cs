using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RocksTTCCreateEffect : IRocksTTCEffect {
  public readonly int id;
  public RocksTTCCreateEffect(int id) {
    this.id = id;
  }
  int IRocksTTCEffect.id => id;
  public void visit(IRocksTTCEffectVisitor visitor) {
    visitor.visitRocksTTCCreateEffect(this);
  }
}

}
