using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounterImpulseStrongMutSetEffect {
  int id { get; }
  void visit(ICounterImpulseStrongMutSetEffectVisitor visitor);
}

}
