using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRavashrikeLevelControllerEffect {
  int id { get; }
  void visit(IRavashrikeLevelControllerEffectVisitor visitor);
}
       
}
