using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveWallTTCCreateEffect : ICaveWallTTCEffect {
  public readonly int id;
  public CaveWallTTCCreateEffect(int id) {
    this.id = id;
  }
  int ICaveWallTTCEffect.id => id;
  public void visit(ICaveWallTTCEffectVisitor visitor) {
    visitor.visitCaveWallTTCCreateEffect(this);
  }
}

}
