using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetAddEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CounteringUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visit(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetAddEffect(this);
  }
}

}
