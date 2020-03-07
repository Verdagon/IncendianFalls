using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianTTCMutSetCreateEffect : IObsidianTTCMutSetEffect {
  public readonly int id;
  public ObsidianTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IObsidianTTCMutSetEffect.id => id;
  public void visit(IObsidianTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetCreateEffect(this);
  }
}

}
