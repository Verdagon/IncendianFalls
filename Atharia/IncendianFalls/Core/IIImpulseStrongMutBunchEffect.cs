using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIImpulseStrongMutBunchEffect {
  int id { get; }
  void visit(IIImpulseStrongMutBunchEffectVisitor visitor);
}
       
}
