using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICaveLevelControllerEffect {
  int id { get; }
  void visit(ICaveLevelControllerEffectVisitor visitor);
}
       
}
