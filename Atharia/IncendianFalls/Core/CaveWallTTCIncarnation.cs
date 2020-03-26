using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveWallTTCIncarnation : ICaveWallTTCEffectVisitor {
  public CaveWallTTCIncarnation(
) {
  }
  public CaveWallTTCIncarnation Copy() {
    return new CaveWallTTCIncarnation(
    );
  }

  public void visitCaveWallTTCCreateEffect(CaveWallTTCCreateEffect e) {}
  public void visitCaveWallTTCDeleteEffect(CaveWallTTCDeleteEffect e) {}

  public void ApplyEffect(ICaveWallTTCEffect effect) { effect.visitICaveWallTTCEffect(this); }
}

}
