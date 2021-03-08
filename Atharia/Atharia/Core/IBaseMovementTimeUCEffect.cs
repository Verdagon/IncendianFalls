using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseMovementTimeUCEffect : IEffect {
  int id { get; }
  void visitIBaseMovementTimeUCEffect(IBaseMovementTimeUCEffectVisitor visitor);
}
       
}
