using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefyImpulseEffect : IEffect {
  int id { get; }
  void visitIDefyImpulseEffect(IDefyImpulseEffectVisitor visitor);
}
       
}
