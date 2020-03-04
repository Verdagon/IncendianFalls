using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FloorTTCMutSetRemoveEffect : IFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FloorTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFloorTTCMutSetEffect.id => id;
  public void visit(IFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetRemoveEffect(this);
  }
}

}
