using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGauntletLevelControllerEffect {
  int id { get; }
  void visit(IGauntletLevelControllerEffectVisitor visitor);
}
       
}
