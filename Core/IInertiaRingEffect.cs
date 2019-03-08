using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IInertiaRingEffect {
  int id { get; }
  void visit(IInertiaRingEffectVisitor visitor);
}
       
}
