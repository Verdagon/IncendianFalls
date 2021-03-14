using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IExplosionRodStrongMutSetEffectVisitor {
  void visitExplosionRodStrongMutSetCreateEffect(ExplosionRodStrongMutSetCreateEffect effect);
  void visitExplosionRodStrongMutSetDeleteEffect(ExplosionRodStrongMutSetDeleteEffect effect);
  void visitExplosionRodStrongMutSetAddEffect(ExplosionRodStrongMutSetAddEffect effect);
  void visitExplosionRodStrongMutSetRemoveEffect(ExplosionRodStrongMutSetRemoveEffect effect);
}
         
}
