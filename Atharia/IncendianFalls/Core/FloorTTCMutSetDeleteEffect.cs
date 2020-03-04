using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FloorTTCMutSetDeleteEffect : IFloorTTCMutSetEffect {
  public readonly int id;
  public FloorTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFloorTTCMutSetEffect.id => id;
  public void visit(IFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetDeleteEffect(this);
  }
}

}
