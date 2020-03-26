using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WallTTCMutSetCreateEffect : IWallTTCMutSetEffect {
  public readonly int id;
  public WallTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IWallTTCMutSetEffect.id => id;
  public void visitIWallTTCMutSetEffect(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWallTTCMutSetEffect(this);
  }
}

}
