using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireImpulseEffect : IEffect {
  int id { get; }
  void visitIFireImpulseEffect(IFireImpulseEffectVisitor visitor);
}
       
}
