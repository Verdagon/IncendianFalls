using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IMoveImpulseStrongMutSetEffectVisitor visitor);
}

}
