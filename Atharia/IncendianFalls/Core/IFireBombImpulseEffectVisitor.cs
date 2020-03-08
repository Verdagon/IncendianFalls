using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombImpulseEffectVisitor {
  void visitFireBombImpulseCreateEffect(FireBombImpulseCreateEffect effect);
  void visitFireBombImpulseDeleteEffect(FireBombImpulseDeleteEffect effect);
}

}
