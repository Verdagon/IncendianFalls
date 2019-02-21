using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIUnitComponentMutBunchEffect {
  int id { get; }
  void visit(IIUnitComponentMutBunchEffectVisitor visitor);
}
       
}
