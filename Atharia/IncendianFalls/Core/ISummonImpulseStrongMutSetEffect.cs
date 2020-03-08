using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonImpulseStrongMutSetEffect {
  int id { get; }
  void visit(ISummonImpulseStrongMutSetEffectVisitor visitor);
}

}
