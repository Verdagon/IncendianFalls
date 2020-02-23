using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WallTTCCreateEffect : IWallTTCEffect {
  public readonly int id;
  public WallTTCCreateEffect(int id) {
    this.id = id;
  }
  int IWallTTCEffect.id => id;
  public void visit(IWallTTCEffectVisitor visitor) {
    visitor.visitWallTTCCreateEffect(this);
  }
}

}
