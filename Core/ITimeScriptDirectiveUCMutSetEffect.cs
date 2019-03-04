using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeScriptDirectiveUCMutSetEffect {
  int id { get; }
  void visit(ITimeScriptDirectiveUCMutSetEffectVisitor visitor);
}

}
