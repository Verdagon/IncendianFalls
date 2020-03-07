using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCMutSetDeleteEffect : IMiredUCMutSetEffect {
  public readonly int id;
  public MiredUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMiredUCMutSetEffect.id => id;
  public void visit(IMiredUCMutSetEffectVisitor visitor) {
    visitor.visitMiredUCMutSetDeleteEffect(this);
  }
}

}
