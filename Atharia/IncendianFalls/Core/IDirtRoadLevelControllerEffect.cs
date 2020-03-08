using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDirtRoadLevelControllerEffect {
  int id { get; }
  void visit(IDirtRoadLevelControllerEffectVisitor visitor);
}
       
}
