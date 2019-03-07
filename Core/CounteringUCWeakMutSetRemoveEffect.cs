using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetRemoveEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CounteringUCWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visit(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetRemoveEffect(this);
  }
}

}
