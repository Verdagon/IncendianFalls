using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IShieldingUCEffect {
  int id { get; }
  void visit(IShieldingUCEffectVisitor visitor);
}
       
}
