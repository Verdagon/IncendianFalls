using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRidgeLevelControllerEffect {
  int id { get; }
  void visit(IRidgeLevelControllerEffectVisitor visitor);
}
       
}
