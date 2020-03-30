using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ObsidianFloorTTCCreateEffect : IObsidianFloorTTCEffect {
  public readonly int id;
  public readonly ObsidianFloorTTCIncarnation incarnation;
  public ObsidianFloorTTCCreateEffect(int id, ObsidianFloorTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IObsidianFloorTTCEffect.id => id;
  public void visitIObsidianFloorTTCEffect(IObsidianFloorTTCEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
