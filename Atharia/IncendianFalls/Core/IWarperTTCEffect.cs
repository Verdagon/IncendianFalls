using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWarperTTCEffect : IEffect {
  int id { get; }
  void visitIWarperTTCEffect(IWarperTTCEffectVisitor visitor);
}
       
}
