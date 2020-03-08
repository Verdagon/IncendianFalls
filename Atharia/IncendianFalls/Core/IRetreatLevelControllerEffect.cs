using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRetreatLevelControllerEffect {
  int id { get; }
  void visit(IRetreatLevelControllerEffectVisitor visitor);
}
       
}
