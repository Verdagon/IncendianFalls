using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPostActingUnitComponentMutBunchEffect {
  int id { get; }
  void visit(IIPostActingUnitComponentMutBunchEffectVisitor visitor);
}
       
}
