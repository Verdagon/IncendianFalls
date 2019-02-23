using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KillDirectiveUCMutSetDeleteEffect : IKillDirectiveUCMutSetEffect {
  public readonly int id;
  public KillDirectiveUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IKillDirectiveUCMutSetEffect.id => id;
  public void visit(IKillDirectiveUCMutSetEffectVisitor visitor) {
    visitor.visitKillDirectiveUCMutSetDeleteEffect(this);
  }
}

}
