using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface INestLevelControllerEffect {
  int id { get; }
  void visit(INestLevelControllerEffectVisitor visitor);
}
       
}
