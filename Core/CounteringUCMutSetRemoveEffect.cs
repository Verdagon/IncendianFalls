using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCMutSetRemoveEffect : ICounteringUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CounteringUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICounteringUCMutSetEffect.id => id;
  public void visit(ICounteringUCMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetRemoveEffect(this);
  }
}

}
