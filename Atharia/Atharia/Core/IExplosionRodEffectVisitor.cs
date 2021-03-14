using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IExplosionRodEffectVisitor {
  void visitExplosionRodCreateEffect(ExplosionRodCreateEffect effect);
  void visitExplosionRodDeleteEffect(ExplosionRodDeleteEffect effect);
}

}
