using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILightningChargingUCEffect : IEffect {
  int id { get; }
  void visitILightningChargingUCEffect(ILightningChargingUCEffectVisitor visitor);
}
       
}
