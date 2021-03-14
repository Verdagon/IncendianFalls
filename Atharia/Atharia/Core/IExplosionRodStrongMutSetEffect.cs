using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IExplosionRodStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIExplosionRodStrongMutSetEffect(IExplosionRodStrongMutSetEffectVisitor visitor);
}

}
