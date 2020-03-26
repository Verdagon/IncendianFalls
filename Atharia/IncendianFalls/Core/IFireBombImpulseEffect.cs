using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireBombImpulseEffect : IEffect {
  int id { get; }
  void visitIFireBombImpulseEffect(IFireBombImpulseEffectVisitor visitor);
}
       
}
