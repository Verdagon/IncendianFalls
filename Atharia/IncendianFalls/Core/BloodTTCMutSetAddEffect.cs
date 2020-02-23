using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BloodTTCMutSetAddEffect : IBloodTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BloodTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBloodTTCMutSetEffect.id => id;
  public void visit(IBloodTTCMutSetEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetAddEffect(this);
  }
}

}
