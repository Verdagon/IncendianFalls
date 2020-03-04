using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWaterTTCMutSetEffectVisitor {
  void visitWaterTTCMutSetCreateEffect(WaterTTCMutSetCreateEffect effect);
  void visitWaterTTCMutSetDeleteEffect(WaterTTCMutSetDeleteEffect effect);
  void visitWaterTTCMutSetAddEffect(WaterTTCMutSetAddEffect effect);
  void visitWaterTTCMutSetRemoveEffect(WaterTTCMutSetRemoveEffect effect);
}
         
}
