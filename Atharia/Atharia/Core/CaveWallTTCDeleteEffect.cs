using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveWallTTCDeleteEffect : ICaveWallTTCEffect {
  public readonly int id;
  public CaveWallTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ICaveWallTTCEffect.id => id;
  public void visitICaveWallTTCEffect(ICaveWallTTCEffectVisitor visitor) {
    visitor.visitCaveWallTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveWallTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
