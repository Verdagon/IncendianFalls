using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianFloorTTCMutSetRemoveEffect : IObsidianFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ObsidianFloorTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IObsidianFloorTTCMutSetEffect.id => id;
  public void visitIObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetEffect(this);
  }
}

}
