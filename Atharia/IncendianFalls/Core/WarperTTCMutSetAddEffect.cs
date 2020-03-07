using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetAddEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WarperTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visit(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetAddEffect(this);
  }
}

}
