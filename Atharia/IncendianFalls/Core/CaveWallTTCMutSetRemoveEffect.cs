using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveWallTTCMutSetRemoveEffect : ICaveWallTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CaveWallTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICaveWallTTCMutSetEffect.id => id;
  public void visit(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetRemoveEffect(this);
  }
}

}
