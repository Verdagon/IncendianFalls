using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IExplosionRodEffect : IEffect {
  int id { get; }
  void visitIExplosionRodEffect(IExplosionRodEffectVisitor visitor);
}
       
}
