using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveWallTTCMutSetAddEffect : ICaveWallTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CaveWallTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICaveWallTTCMutSetEffect.id => id;
  public void visitICaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
