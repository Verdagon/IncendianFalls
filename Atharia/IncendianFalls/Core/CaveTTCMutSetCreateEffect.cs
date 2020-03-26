using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetCreateEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public CaveTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visitICaveTTCMutSetEffect(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetEffect(this);
  }
}

}
