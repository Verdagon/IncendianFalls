using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BloodTTCMutSetDeleteEffect : IBloodTTCMutSetEffect {
  public readonly int id;
  public BloodTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBloodTTCMutSetEffect.id => id;
  public void visitIBloodTTCMutSetEffect(IBloodTTCMutSetEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetEffect(this);
  }
}

}
