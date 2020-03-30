using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianFloorTTCMutSetAddEffect : IObsidianFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ObsidianFloorTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IObsidianFloorTTCMutSetEffect.id => id;
  public void visitIObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
