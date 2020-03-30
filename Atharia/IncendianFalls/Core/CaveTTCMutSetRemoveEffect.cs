using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetRemoveEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CaveTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visitICaveTTCMutSetEffect(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
