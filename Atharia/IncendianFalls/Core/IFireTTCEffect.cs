using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireTTCEffect : IEffect {
  int id { get; }
  void visitIFireTTCEffect(IFireTTCEffectVisitor visitor);
}
       
}
