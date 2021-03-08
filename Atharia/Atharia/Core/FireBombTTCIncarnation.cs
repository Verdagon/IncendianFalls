using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombTTCIncarnation : IFireBombTTCEffectVisitor {
  public  int turnsUntilExplosion;
  public FireBombTTCIncarnation(
      int turnsUntilExplosion) {
    this.turnsUntilExplosion = turnsUntilExplosion;
  }
  public FireBombTTCIncarnation Copy() {
    return new FireBombTTCIncarnation(
turnsUntilExplosion    );
  }

  public void visitFireBombTTCCreateEffect(FireBombTTCCreateEffect e) {}
  public void visitFireBombTTCDeleteEffect(FireBombTTCDeleteEffect e) {}
public void visitFireBombTTCSetTurnsUntilExplosionEffect(FireBombTTCSetTurnsUntilExplosionEffect e) { this.turnsUntilExplosion = e.newValue; }
  public void ApplyEffect(IFireBombTTCEffect effect) { effect.visitIFireBombTTCEffect(this); }
}

}
