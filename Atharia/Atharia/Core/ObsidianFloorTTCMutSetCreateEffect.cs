using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianFloorTTCMutSetCreateEffect : IObsidianFloorTTCMutSetEffect {
  public readonly int id;
  public ObsidianFloorTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IObsidianFloorTTCMutSetEffect.id => id;
  public void visitIObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
