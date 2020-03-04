using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWaterTTCEffect {
  int id { get; }
  void visit(IWaterTTCEffectVisitor visitor);
}
       
}
