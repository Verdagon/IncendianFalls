using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetCreateEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public WarperTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visitIWarperTTCMutSetEffect(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetEffect(this);
  }
}

}
