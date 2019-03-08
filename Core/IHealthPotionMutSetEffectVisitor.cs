using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionMutSetEffectVisitor {
  void visitHealthPotionMutSetCreateEffect(HealthPotionMutSetCreateEffect effect);
  void visitHealthPotionMutSetDeleteEffect(HealthPotionMutSetDeleteEffect effect);
  void visitHealthPotionMutSetAddEffect(HealthPotionMutSetAddEffect effect);
  void visitHealthPotionMutSetRemoveEffect(HealthPotionMutSetRemoveEffect effect);
}
         
}
