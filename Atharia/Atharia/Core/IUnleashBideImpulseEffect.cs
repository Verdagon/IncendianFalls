using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnleashBideImpulseEffect : IEffect {
  int id { get; }
  void visitIUnleashBideImpulseEffect(IUnleashBideImpulseEffectVisitor visitor);
}
       
}
