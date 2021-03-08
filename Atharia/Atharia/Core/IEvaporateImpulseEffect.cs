using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IEvaporateImpulseEffect : IEffect {
  int id { get; }
  void visitIEvaporateImpulseEffect(IEvaporateImpulseEffectVisitor visitor);
}
       
}
