using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGlaiveEffect : IEffect {
  int id { get; }
  void visitIGlaiveEffect(IGlaiveEffectVisitor visitor);
}
       
}
