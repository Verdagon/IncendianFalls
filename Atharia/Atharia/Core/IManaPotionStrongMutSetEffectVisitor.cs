using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionStrongMutSetEffectVisitor {
  void visitManaPotionStrongMutSetCreateEffect(ManaPotionStrongMutSetCreateEffect effect);
  void visitManaPotionStrongMutSetDeleteEffect(ManaPotionStrongMutSetDeleteEffect effect);
  void visitManaPotionStrongMutSetAddEffect(ManaPotionStrongMutSetAddEffect effect);
  void visitManaPotionStrongMutSetRemoveEffect(ManaPotionStrongMutSetRemoveEffect effect);
}
         
}
