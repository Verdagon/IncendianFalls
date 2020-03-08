using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianFloorTTCMutSetDeleteEffect : IObsidianFloorTTCMutSetEffect {
  public readonly int id;
  public ObsidianFloorTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IObsidianFloorTTCMutSetEffect.id => id;
  public void visit(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetDeleteEffect(this);
  }
}

}
