using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetDeleteEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public WarperTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visitIWarperTTCMutSetEffect(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetEffect(this);
  }
}

}
