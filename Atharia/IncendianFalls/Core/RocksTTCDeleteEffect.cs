using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RocksTTCDeleteEffect : IRocksTTCEffect {
  public readonly int id;
  public RocksTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IRocksTTCEffect.id => id;
  public void visit(IRocksTTCEffectVisitor visitor) {
    visitor.visitRocksTTCDeleteEffect(this);
  }
}

}
