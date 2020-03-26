using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WallTTCMutSetRemoveEffect : IWallTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WallTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWallTTCMutSetEffect.id => id;
  public void visitIWallTTCMutSetEffect(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWallTTCMutSetEffect(this);
  }
}

}
