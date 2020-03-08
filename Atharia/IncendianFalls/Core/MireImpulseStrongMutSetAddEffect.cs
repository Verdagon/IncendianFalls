using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MireImpulseStrongMutSetAddEffect : IMireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MireImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMireImpulseStrongMutSetEffect.id => id;
  public void visit(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetAddEffect(this);
  }
}

}
