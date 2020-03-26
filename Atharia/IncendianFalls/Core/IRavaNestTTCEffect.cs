using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRavaNestTTCEffect : IEffect {
  int id { get; }
  void visitIRavaNestTTCEffect(IRavaNestTTCEffectVisitor visitor);
}
       
}
