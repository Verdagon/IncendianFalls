using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionStrongMutSetEffectVisitor {
  void visitHealthPotionStrongMutSetCreateEffect(HealthPotionStrongMutSetCreateEffect effect);
  void visitHealthPotionStrongMutSetDeleteEffect(HealthPotionStrongMutSetDeleteEffect effect);
  void visitHealthPotionStrongMutSetAddEffect(HealthPotionStrongMutSetAddEffect effect);
  void visitHealthPotionStrongMutSetRemoveEffect(HealthPotionStrongMutSetRemoveEffect effect);
}
         
}
