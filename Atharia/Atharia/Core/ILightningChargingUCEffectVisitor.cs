using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargingUCEffectVisitor {
  void visitLightningChargingUCCreateEffect(LightningChargingUCCreateEffect effect);
  void visitLightningChargingUCDeleteEffect(LightningChargingUCDeleteEffect effect);
}

}
