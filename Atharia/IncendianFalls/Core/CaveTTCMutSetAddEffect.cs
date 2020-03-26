using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetAddEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CaveTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visitICaveTTCMutSetEffect(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetEffect(this);
  }
}

}
