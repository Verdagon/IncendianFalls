using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMoveImpulseEffect : IEffect {
  int id { get; }
  void visitIMoveImpulseEffect(IMoveImpulseEffectVisitor visitor);
}
       
}
