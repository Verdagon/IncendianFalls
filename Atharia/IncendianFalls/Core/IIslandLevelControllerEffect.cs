using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIslandLevelControllerEffect {
  int id { get; }
  void visit(IIslandLevelControllerEffectVisitor visitor);
}
       
}
