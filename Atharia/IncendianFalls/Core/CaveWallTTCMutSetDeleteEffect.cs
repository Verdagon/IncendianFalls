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
  public void visit(ICaveWallTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveWallTTCMutSetDeleteEffect(this);
  }
}

}
