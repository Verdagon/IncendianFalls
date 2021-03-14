using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IExplosionRodMutSetEffect : IEffect {
  int id { get; }
  void visitIExplosionRodMutSetEffect(IExplosionRodMutSetEffectVisitor visitor);
}

}
