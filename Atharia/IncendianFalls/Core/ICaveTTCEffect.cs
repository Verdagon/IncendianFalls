using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICaveTTCEffect : IEffect {
  int id { get; }
  void visitICaveTTCEffect(ICaveTTCEffectVisitor visitor);
}
       
}
