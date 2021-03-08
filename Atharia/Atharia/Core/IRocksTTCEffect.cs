using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRocksTTCEffect : IEffect {
  int id { get; }
  void visitIRocksTTCEffect(IRocksTTCEffectVisitor visitor);
}
       
}
