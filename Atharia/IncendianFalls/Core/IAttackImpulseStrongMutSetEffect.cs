using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIAttackImpulseStrongMutSetEffect(IAttackImpulseStrongMutSetEffectVisitor visitor);
}

}
