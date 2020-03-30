using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WallTTCDeleteEffect : IWallTTCEffect {
  public readonly int id;
  public WallTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IWallTTCEffect.id => id;
  public void visitIWallTTCEffect(IWallTTCEffectVisitor visitor) {
    visitor.visitWallTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWallTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
