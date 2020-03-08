using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IAttackImpulseStrongMutSetEffectVisitor visitor);
}

}
