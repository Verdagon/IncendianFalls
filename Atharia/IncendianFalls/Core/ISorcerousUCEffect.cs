using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISorcerousUCEffect : IEffect {
  int id { get; }
  void visitISorcerousUCEffect(ISorcerousUCEffectVisitor visitor);
}
       
}
