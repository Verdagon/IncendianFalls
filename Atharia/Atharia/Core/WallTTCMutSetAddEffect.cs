using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WallTTCMutSetAddEffect : IWallTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WallTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWallTTCMutSetEffect.id => id;
  public void visitIWallTTCMutSetEffect(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWallTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
