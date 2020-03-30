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
  public void visitIRocksTTCEffect(IRocksTTCEffectVisitor visitor) {
    visitor.visitRocksTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRocksTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
