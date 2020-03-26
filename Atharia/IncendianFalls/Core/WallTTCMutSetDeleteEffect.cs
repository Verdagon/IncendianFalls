using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WallTTCMutSetDeleteEffect : IWallTTCMutSetEffect {
  public readonly int id;
  public WallTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IWallTTCMutSetEffect.id => id;
  public void visitIWallTTCMutSetEffect(IWallTTCMutSetEffectVisitor visitor) {
    visitor.visitWallTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWallTTCMutSetEffect(this);
  }
}

}
