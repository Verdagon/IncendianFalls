using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILakeLevelControllerEffect {
  int id { get; }
  void visit(ILakeLevelControllerEffectVisitor visitor);
}
       
}
