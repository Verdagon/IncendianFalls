using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRandEffect : IEffect {
  int id { get; }
  void visitIRandEffect(IRandEffectVisitor visitor);
}
       
}
