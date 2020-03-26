using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDoomedUCEffect : IEffect {
  int id { get; }
  void visitIDoomedUCEffect(IDoomedUCEffectVisitor visitor);
}
       
}
