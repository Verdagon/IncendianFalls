using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WarperTTCMutSetAddEffect : IWarperTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WarperTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWarperTTCMutSetEffect.id => id;
  public void visitIWarperTTCMutSetEffect(IWarperTTCMutSetEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWarperTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
