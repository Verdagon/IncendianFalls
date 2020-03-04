using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetDeleteEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public CaveTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visit(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetDeleteEffect(this);
  }
}

}