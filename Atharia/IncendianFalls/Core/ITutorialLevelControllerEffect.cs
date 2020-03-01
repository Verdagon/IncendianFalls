using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITutorialLevelControllerEffect {
  int id { get; }
  void visit(ITutorialLevelControllerEffectVisitor visitor);
}
       
}
