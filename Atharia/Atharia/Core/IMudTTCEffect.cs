using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMudTTCEffect : IEffect {
  int id { get; }
  void visitIMudTTCEffect(IMudTTCEffectVisitor visitor);
}
       
}
