using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WallTTCIncarnation : IWallTTCEffectVisitor {
  public WallTTCIncarnation(
) {
  }
  public WallTTCIncarnation Copy() {
    return new WallTTCIncarnation(
    );
  }

  public void visitWallTTCCreateEffect(WallTTCCreateEffect e) {}
  public void visitWallTTCDeleteEffect(WallTTCDeleteEffect e) {}

  public void ApplyEffect(IWallTTCEffect effect) { effect.visitIWallTTCEffect(this); }
}

}
