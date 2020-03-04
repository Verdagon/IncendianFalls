using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FloorTTCCreateEffect : IFloorTTCEffect {
  public readonly int id;
  public FloorTTCCreateEffect(int id) {
    this.id = id;
  }
  int IFloorTTCEffect.id => id;
  public void visit(IFloorTTCEffectVisitor visitor) {
    visitor.visitFloorTTCCreateEffect(this);
  }
}

}
