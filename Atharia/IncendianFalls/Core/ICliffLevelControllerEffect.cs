using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICliffLevelControllerEffect {
  int id { get; }
  void visit(ICliffLevelControllerEffectVisitor visitor);
}
       
}
