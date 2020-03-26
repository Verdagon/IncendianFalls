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
  public void visitIObsidianFloorTTCMutSetEffect(IObsidianFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCMutSetEffect(this);
  }
}

}
