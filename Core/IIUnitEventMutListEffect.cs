using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIUnitEventMutListEffect {
  int id { get; }
  void visit(IIUnitEventMutListEffectVisitor visitor);
}

}
