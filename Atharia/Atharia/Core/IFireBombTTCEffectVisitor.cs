using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombTTCEffectVisitor {
  void visitFireBombTTCCreateEffect(FireBombTTCCreateEffect effect);
  void visitFireBombTTCDeleteEffect(FireBombTTCDeleteEffect effect);
  void visitFireBombTTCSetTurnsUntilExplosionEffect(FireBombTTCSetTurnsUntilExplosionEffect effect);
}

}
