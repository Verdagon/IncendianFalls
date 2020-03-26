using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveWallTTCCreateEffect : ICaveWallTTCEffect {
  public readonly int id;
  public readonly CaveWallTTCIncarnation incarnation;
  public CaveWallTTCCreateEffect(int id, CaveWallTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICaveWallTTCEffect.id => id;
  public void visitICaveWallTTCEffect(ICaveWallTTCEffectVisitor visitor) {
    visitor.visitCaveWallTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveWallTTCEffect(this);
  }
}

}
