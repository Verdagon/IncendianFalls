using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WallTTCCreateEffect : IWallTTCEffect {
  public readonly int id;
  public readonly WallTTCIncarnation incarnation;
  public WallTTCCreateEffect(int id, WallTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IWallTTCEffect.id => id;
  public void visitIWallTTCEffect(IWallTTCEffectVisitor visitor) {
    visitor.visitWallTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWallTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
