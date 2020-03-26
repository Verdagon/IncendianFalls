using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RocksTTCMutSetDeleteEffect : IRocksTTCMutSetEffect {
  public readonly int id;
  public RocksTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IRocksTTCMutSetEffect.id => id;
  public void visitIRocksTTCMutSetEffect(IRocksTTCMutSetEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetEffect(this);
  }
}

}
