using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KillDirectiveDeleteEffect : IKillDirectiveEffect {
  public readonly int id;
  public KillDirectiveDeleteEffect(int id) {
    this.id = id;
  }
  int IKillDirectiveEffect.id => id;
  public void visit(IKillDirectiveEffectVisitor visitor) {
    visitor.visitKillDirectiveDeleteEffect(this);
  }
}

}
