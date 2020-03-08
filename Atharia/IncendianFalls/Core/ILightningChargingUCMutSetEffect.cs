using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargingUCMutSetEffect {
  int id { get; }
  void visit(ILightningChargingUCMutSetEffectVisitor visitor);
}

}
