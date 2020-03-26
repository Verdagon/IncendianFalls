using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianTTCMutSetRemoveEffect : IObsidianTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ObsidianTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IObsidianTTCMutSetEffect.id => id;
  public void visitIObsidianTTCMutSetEffect(IObsidianTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetEffect(this);
  }
}

}
