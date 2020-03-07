using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCWeakMutSetDeleteEffect : IMiredUCWeakMutSetEffect {
  public readonly int id;
  public MiredUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMiredUCWeakMutSetEffect.id => id;
  public void visit(IMiredUCWeakMutSetEffectVisitor visitor) {
    visitor.visitMiredUCWeakMutSetDeleteEffect(this);
  }
}

}
