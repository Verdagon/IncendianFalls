using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombTTCSetTurnsUntilExplosionEffect : IFireBombTTCEffect {
  public readonly int id;
  public readonly int newValue;
  public FireBombTTCSetTurnsUntilExplosionEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IFireBombTTCEffect.id => id;

  public void visit(IFireBombTTCEffectVisitor visitor) {
    visitor.visitFireBombTTCSetTurnsUntilExplosionEffect(this);
  }
}

}
