using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMiredUCEffect {
  int id { get; }
  void visit(IMiredUCEffectVisitor visitor);
}
       
}
