using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IFireImpulseStrongMutSetEffectVisitor visitor);
}

}
