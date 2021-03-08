using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface INoImpulseEffect : IEffect {
  int id { get; }
  void visitINoImpulseEffect(INoImpulseEffectVisitor visitor);
}
       
}
