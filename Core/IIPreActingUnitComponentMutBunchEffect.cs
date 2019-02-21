using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPreActingUnitComponentMutBunchEffect {
  int id { get; }
  void visit(IIPreActingUnitComponentMutBunchEffectVisitor visitor);
}
       
}
