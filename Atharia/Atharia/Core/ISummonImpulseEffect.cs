using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISummonImpulseEffect : IEffect {
  int id { get; }
  void visitISummonImpulseEffect(ISummonImpulseEffectVisitor visitor);
}
       
}
