using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITimeScriptDirectiveUCEffect {
  int id { get; }
  void visit(ITimeScriptDirectiveUCEffectVisitor visitor);
}
       
}
