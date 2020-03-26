using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IHoldPositionImpulseEffect : IEffect {
  int id { get; }
  void visitIHoldPositionImpulseEffect(IHoldPositionImpulseEffectVisitor visitor);
}
       
}
