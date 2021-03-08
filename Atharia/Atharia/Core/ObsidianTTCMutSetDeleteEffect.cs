using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ObsidianTTCMutSetDeleteEffect : IObsidianTTCMutSetEffect {
  public readonly int id;
  public ObsidianTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IObsidianTTCMutSetEffect.id => id;
  public void visitIObsidianTTCMutSetEffect(IObsidianTTCMutSetEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitObsidianTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
