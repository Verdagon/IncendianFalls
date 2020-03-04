using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FloorTTCMutSetAddEffect : IFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FloorTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFloorTTCMutSetEffect.id => id;
  public void visit(IFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetAddEffect(this);
  }
}

}
