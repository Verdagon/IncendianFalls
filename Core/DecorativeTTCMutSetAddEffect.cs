using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DecorativeTTCMutSetAddEffect : IDecorativeTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DecorativeTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDecorativeTTCMutSetEffect.id => id;
  public void visit(IDecorativeTTCMutSetEffectVisitor visitor) {
    visitor.visitDecorativeTTCMutSetAddEffect(this);
  }
}

}
