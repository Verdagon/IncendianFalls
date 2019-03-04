using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTTCMutSetDeleteEffect : IDecorativeTTCMutSetEffect {
  public readonly int id;
  public DecorativeTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDecorativeTTCMutSetEffect.id => id;
  public void visit(IDecorativeTTCMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTTCMutSetDeleteEffect(this);
  }
}

}
