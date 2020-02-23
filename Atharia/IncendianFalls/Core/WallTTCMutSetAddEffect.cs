using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WallTTCMutSetAddEffect : IWallTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WallTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWallTTCMutSetEffect.id => id;
  public void visit(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetAddEffect(this);
  }
}

}
