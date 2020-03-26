using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianTTCMutSetAddEffect : IObsidianTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ObsidianTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IObsidianTTCMutSetEffect.id => id;
  public void visitIObsidianTTCMutSetEffect(IObsidianTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetEffect(this);
  }
}

}
