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
  public void visit(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetCreateEffect(this);
  }
}

}
