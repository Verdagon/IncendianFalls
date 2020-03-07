using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ObsidianTTCCreateEffect : IObsidianTTCEffect {
  public readonly int id;
  public ObsidianTTCCreateEffect(int id) {
    this.id = id;
  }
  int IObsidianTTCEffect.id => id;
  public void visit(IObsidianTTCEffectVisitor visitor) {
    visitor.visitObsidianTTCCreateEffect(this);
  }
}

}
