using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StaircaseTTCMutSetRemoveEffect : IStaircaseTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public StaircaseTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IStaircaseTTCMutSetEffect.id => id;
  public void visit(IStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitStaircaseTTCMutSetRemoveEffect(this);
  }
}

}
