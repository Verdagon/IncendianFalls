using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetRemoveEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WarperTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visitIWarperTTCMutSetEffect(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetEffect(this);
  }
}

}
