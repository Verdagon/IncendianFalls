using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKillDirectiveUCEffect {
  int id { get; }
  void visit(IKillDirectiveUCEffectVisitor visitor);
}
       
}
