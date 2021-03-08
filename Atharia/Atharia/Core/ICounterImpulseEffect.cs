using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICounterImpulseEffect : IEffect {
  int id { get; }
  void visitICounterImpulseEffect(ICounterImpulseEffectVisitor visitor);
}
       
}
