using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireTTCMutSetAddEffect : IFireTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FireTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFireTTCMutSetEffect.id => id;
  public void visit(IFireTTCMutSetEffectVisitor visitor) {
    visitor.visitFireTTCMutSetAddEffect(this);
  }
}

}
