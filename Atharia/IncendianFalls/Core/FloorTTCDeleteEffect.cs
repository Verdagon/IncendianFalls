using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FloorTTCDeleteEffect : IFloorTTCEffect {
  public readonly int id;
  public FloorTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IFloorTTCEffect.id => id;
  public void visit(IFloorTTCEffectVisitor visitor) {
    visitor.visitFloorTTCDeleteEffect(this);
  }
}

}
