using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveWallTTCMutSetCreateEffect : ICaveWallTTCMutSetEffect {
  public readonly int id;
  public CaveWallTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICaveWallTTCMutSetEffect.id => id;
  public void visitICaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetEffect(this);
  }
}

}
