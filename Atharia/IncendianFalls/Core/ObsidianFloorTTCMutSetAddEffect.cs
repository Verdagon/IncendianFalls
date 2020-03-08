using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianFloorTTCMutSetAddEffect : IObsidianFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ObsidianFloorTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IObsidianFloorTTCMutSetEffect.id => id;
  public void visit(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetAddEffect(this);
  }
}

}
