using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILightningChargingUCEffect {
  int id { get; }
  void visit(ILightningChargingUCEffectVisitor visitor);
}
       
}
