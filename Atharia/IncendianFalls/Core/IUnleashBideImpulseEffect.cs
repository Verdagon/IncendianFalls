using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnleashBideImpulseEffect {
  int id { get; }
  void visit(IUnleashBideImpulseEffectVisitor visitor);
}
       
}
