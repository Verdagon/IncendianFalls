using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIncendianFallsLevelLinkerTTCEffect {
  int id { get; }
  void visit(IIncendianFallsLevelLinkerTTCEffectVisitor visitor);
}
       
}
