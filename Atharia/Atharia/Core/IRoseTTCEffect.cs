using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRoseTTCEffect : IEffect {
  int id { get; }
  void visitIRoseTTCEffect(IRoseTTCEffectVisitor visitor);
}
       
}
