using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCWeakMutSetRemoveEffect : IMiredUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MiredUCWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMiredUCWeakMutSetEffect.id => id;
  public void visit(IMiredUCWeakMutSetEffectVisitor visitor) {
    visitor.visitMiredUCWeakMutSetRemoveEffect(this);
  }
}

}
