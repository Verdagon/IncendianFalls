using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ObsidianFloorTTCCreateEffect : IObsidianFloorTTCEffect {
  public readonly int id;
  public ObsidianFloorTTCCreateEffect(int id) {
    this.id = id;
  }
  int IObsidianFloorTTCEffect.id => id;
  public void visit(IObsidianFloorTTCEffectVisitor visitor) {
    visitor.visitObsidianFloorTTCCreateEffect(this);
  }
}

}
