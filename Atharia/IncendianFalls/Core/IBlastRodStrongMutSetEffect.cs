using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodStrongMutSetEffect {
  int id { get; }
  void visit(IBlastRodStrongMutSetEffectVisitor visitor);
}

}
