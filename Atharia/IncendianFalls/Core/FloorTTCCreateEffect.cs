using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FloorTTCCreateEffect : IFloorTTCEffect {
  public readonly int id;
  public readonly FloorTTCIncarnation incarnation;
  public FloorTTCCreateEffect(int id, FloorTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFloorTTCEffect.id => id;
  public void visitIFloorTTCEffect(IFloorTTCEffectVisitor visitor) {
    visitor.visitFloorTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFloorTTCEffect(this);
  }
}

}
