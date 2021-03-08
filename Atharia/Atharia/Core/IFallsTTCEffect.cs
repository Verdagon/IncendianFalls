using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFallsTTCEffect : IEffect {
  int id { get; }
  void visitIFallsTTCEffect(IFallsTTCEffectVisitor visitor);
}
       
}
