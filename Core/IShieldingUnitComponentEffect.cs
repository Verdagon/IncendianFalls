using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IShieldingUnitComponentEffect {
  int id { get; }
  void visit(IShieldingUnitComponentEffectVisitor visitor);
}
       
}
