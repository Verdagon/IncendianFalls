using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionMutSetEffectVisitor {
  void visitManaPotionMutSetCreateEffect(ManaPotionMutSetCreateEffect effect);
  void visitManaPotionMutSetDeleteEffect(ManaPotionMutSetDeleteEffect effect);
  void visitManaPotionMutSetAddEffect(ManaPotionMutSetAddEffect effect);
  void visitManaPotionMutSetRemoveEffect(ManaPotionMutSetRemoveEffect effect);
}
         
}
