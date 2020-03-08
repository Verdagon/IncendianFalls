using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MireImpulseStrongMutSetDeleteEffect : IMireImpulseStrongMutSetEffect {
  public readonly int id;
  public MireImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMireImpulseStrongMutSetEffect.id => id;
  public void visit(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetDeleteEffect(this);
  }
}

}
