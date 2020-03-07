using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITutorial2LevelControllerEffect {
  int id { get; }
  void visit(ITutorial2LevelControllerEffectVisitor visitor);
}
       
}
