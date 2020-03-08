using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface INoImpulseStrongMutSetEffect {
  int id { get; }
  void visit(INoImpulseStrongMutSetEffectVisitor visitor);
}

}
