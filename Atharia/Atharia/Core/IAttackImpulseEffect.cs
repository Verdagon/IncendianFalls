using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAttackImpulseEffect : IEffect {
  int id { get; }
  void visitIAttackImpulseEffect(IAttackImpulseEffectVisitor visitor);
}
       
}
