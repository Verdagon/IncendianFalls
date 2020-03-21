using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseSightRangeUCEffect {
  int id { get; }
  void visit(IBaseSightRangeUCEffectVisitor visitor);
}
       
}
