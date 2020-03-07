using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianTTCMutSetAddEffect : IObsidianTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ObsidianTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IObsidianTTCMutSetEffect.id => id;
  public void visit(IObsidianTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetAddEffect(this);
  }
}

}
