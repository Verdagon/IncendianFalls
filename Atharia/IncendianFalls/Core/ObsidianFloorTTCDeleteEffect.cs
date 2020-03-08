using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ObsidianFloorTTCDeleteEffect : IObsidianFloorTTCEffect {
  public readonly int id;
  public ObsidianFloorTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IObsidianFloorTTCEffect.id => id;
  public void visit(IObsidianFloorTTCEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCDeleteEffect(this);
  }
}

}