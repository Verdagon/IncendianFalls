using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianFloorTTCMutSetRemoveEffect : IObsidianFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ObsidianFloorTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IObsidianFloorTTCMutSetEffect.id => id;
  public void visit(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetRemoveEffect(this);
  }
}

}
