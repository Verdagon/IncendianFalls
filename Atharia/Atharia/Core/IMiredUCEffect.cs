using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMiredUCEffect : IEffect {
  int id { get; }
  void visitIMiredUCEffect(IMiredUCEffectVisitor visitor);
}
       
}
