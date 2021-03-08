using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargingUCMutSetEffectVisitor {
  void visitLightningChargingUCMutSetCreateEffect(LightningChargingUCMutSetCreateEffect effect);
  void visitLightningChargingUCMutSetDeleteEffect(LightningChargingUCMutSetDeleteEffect effect);
  void visitLightningChargingUCMutSetAddEffect(LightningChargingUCMutSetAddEffect effect);
  void visitLightningChargingUCMutSetRemoveEffect(LightningChargingUCMutSetRemoveEffect effect);
}
         
}
