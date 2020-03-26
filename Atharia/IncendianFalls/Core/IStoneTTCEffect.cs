using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IStoneTTCEffect : IEffect {
  int id { get; }
  void visitIStoneTTCEffect(IStoneTTCEffectVisitor visitor);
}
       
}
