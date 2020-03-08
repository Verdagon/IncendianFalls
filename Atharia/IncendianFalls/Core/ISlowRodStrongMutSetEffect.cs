using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodStrongMutSetEffect {
  int id { get; }
  void visit(ISlowRodStrongMutSetEffectVisitor visitor);
}

}
