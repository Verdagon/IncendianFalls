using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTTCMutSetRemoveEffect : IDecorativeTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DecorativeTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDecorativeTTCMutSetEffect.id => id;
  public void visit(IDecorativeTTCMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTTCMutSetRemoveEffect(this);
  }
}

}
