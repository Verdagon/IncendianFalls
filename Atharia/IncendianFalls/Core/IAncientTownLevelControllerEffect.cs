using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAncientTownLevelControllerEffect {
  int id { get; }
  void visit(IAncientTownLevelControllerEffectVisitor visitor);
}
       
}
