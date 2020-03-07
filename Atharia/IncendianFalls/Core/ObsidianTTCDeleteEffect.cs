using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ObsidianTTCDeleteEffect : IObsidianTTCEffect {
  public readonly int id;
  public ObsidianTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IObsidianTTCEffect.id => id;
  public void visit(IObsidianTTCEffectVisitor visitor) {
    visitor.visitObsidianTTCDeleteEffect(this);
  }
}

}
