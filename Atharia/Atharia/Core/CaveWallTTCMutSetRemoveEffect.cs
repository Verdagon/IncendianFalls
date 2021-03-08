using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveWallTTCMutSetRemoveEffect : ICaveWallTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CaveWallTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICaveWallTTCMutSetEffect.id => id;
  public void visitICaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
