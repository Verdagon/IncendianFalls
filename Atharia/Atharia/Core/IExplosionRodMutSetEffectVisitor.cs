using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IExplosionRodMutSetEffectVisitor {
  void visitExplosionRodMutSetCreateEffect(ExplosionRodMutSetCreateEffect effect);
  void visitExplosionRodMutSetDeleteEffect(ExplosionRodMutSetDeleteEffect effect);
  void visitExplosionRodMutSetAddEffect(ExplosionRodMutSetAddEffect effect);
  void visitExplosionRodMutSetRemoveEffect(ExplosionRodMutSetRemoveEffect effect);
}
         
}
