using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveWallTTCMutSetAddEffect : ICaveWallTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CaveWallTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICaveWallTTCMutSetEffect.id => id;
  public void visit(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetAddEffect(this);
  }
}

}
