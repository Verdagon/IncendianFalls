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
  public void visitIMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetEffect(this);
  }
}

}
