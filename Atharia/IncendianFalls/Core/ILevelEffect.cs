using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelEffect {
  int id { get; }
  void visit(ILevelEffectVisitor visitor);
}
       
}
