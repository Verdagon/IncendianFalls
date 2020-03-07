using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianTTCMutSetRemoveEffect : IObsidianTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ObsidianTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IObsidianTTCMutSetEffect.id => id;
  public void visit(IObsidianTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetRemoveEffect(this);
  }
}

}
