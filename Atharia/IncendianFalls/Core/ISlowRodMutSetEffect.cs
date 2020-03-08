using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodMutSetEffect {
  int id { get; }
  void visit(ISlowRodMutSetEffectVisitor visitor);
}

}
