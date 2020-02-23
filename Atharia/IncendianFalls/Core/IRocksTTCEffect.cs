using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRocksTTCEffect {
  int id { get; }
  void visit(IRocksTTCEffectVisitor visitor);
}
       
}
