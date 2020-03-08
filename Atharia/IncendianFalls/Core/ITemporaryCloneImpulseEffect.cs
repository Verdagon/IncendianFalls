using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITemporaryCloneImpulseEffect {
  int id { get; }
  void visit(ITemporaryCloneImpulseEffectVisitor visitor);
}
       
}
