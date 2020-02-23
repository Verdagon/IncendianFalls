using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WallTTCMutSetRemoveEffect : IWallTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WallTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWallTTCMutSetEffect.id => id;
  public void visit(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetRemoveEffect(this);
  }
}

}
