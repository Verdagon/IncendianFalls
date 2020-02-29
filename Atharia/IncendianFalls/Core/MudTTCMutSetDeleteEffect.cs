using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MudTTCMutSetDeleteEffect : IMudTTCMutSetEffect {
  public readonly int id;
  public MudTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMudTTCMutSetEffect.id => id;
  public void visit(IMudTTCMutSetEffectVisitor visitor) {
    visitor.visitMudTTCMutSetDeleteEffect(this);
  }
}

}
