using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargingUCMutSetEffect : IEffect {
  int id { get; }
  void visitILightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffectVisitor visitor);
}

}
