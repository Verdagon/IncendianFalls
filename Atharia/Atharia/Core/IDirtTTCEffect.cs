using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDirtTTCEffect : IEffect {
  int id { get; }
  void visitIDirtTTCEffect(IDirtTTCEffectVisitor visitor);
}
       
}
