using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IPursueImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IPursueImpulseStrongMutSetEffectVisitor visitor);
}

}
