using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodMutSetEffect {
  int id { get; }
  void visit(IBlastRodMutSetEffectVisitor visitor);
}

}
