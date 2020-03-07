using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITutorial1LevelControllerEffect {
  int id { get; }
  void visit(ITutorial1LevelControllerEffectVisitor visitor);
}
       
}
