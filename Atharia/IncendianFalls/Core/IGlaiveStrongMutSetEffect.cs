using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveStrongMutSetEffect {
  int id { get; }
  void visit(IGlaiveStrongMutSetEffectVisitor visitor);
}

}
