using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKillDirectiveEffect {
  int id { get; }
  void visit(IKillDirectiveEffectVisitor visitor);
}
       
}
