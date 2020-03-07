using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetRemoveEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WarperTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visit(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetRemoveEffect(this);
  }
}

}
