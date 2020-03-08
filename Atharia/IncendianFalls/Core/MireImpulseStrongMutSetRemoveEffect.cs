using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MireImpulseStrongMutSetRemoveEffect : IMireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MireImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMireImpulseStrongMutSetEffect.id => id;
  public void visit(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetRemoveEffect(this);
  }
}

}
