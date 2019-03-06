using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPentagonalCaveLevelControllerEffect {
  int id { get; }
  void visit(IPentagonalCaveLevelControllerEffectVisitor visitor);
}
       
}
