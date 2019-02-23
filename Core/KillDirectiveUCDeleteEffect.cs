using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KillDirectiveUCDeleteEffect : IKillDirectiveUCEffect {
  public readonly int id;
  public KillDirectiveUCDeleteEffect(int id) {
    this.id = id;
  }
  int IKillDirectiveUCEffect.id => id;
  public void visit(IKillDirectiveUCEffectVisitor visitor) {
    visitor.visitKillDirectiveUCDeleteEffect(this);
  }
}
       
}
