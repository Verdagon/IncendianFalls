using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMireImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IMireImpulseStrongMutSetEffectVisitor visitor);
}

}
