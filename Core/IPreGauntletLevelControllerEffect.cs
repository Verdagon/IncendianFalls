using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPreGauntletLevelControllerEffect {
  int id { get; }
  void visit(IPreGauntletLevelControllerEffectVisitor visitor);
}
       
}
