using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IDefyImpulseStrongMutSetEffectVisitor visitor);
}

}
