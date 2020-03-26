using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveWallTTCMutSetDeleteEffect : ICaveWallTTCMutSetEffect {
  public readonly int id;
  public CaveWallTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICaveWallTTCMutSetEffect.id => id;
  public void visitICaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetEffect(this);
  }
}

}
