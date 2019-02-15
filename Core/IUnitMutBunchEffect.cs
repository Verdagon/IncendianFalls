using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitMutBunchEffect {
  int id { get; }
  void visit(IUnitMutBunchEffectVisitor visitor);
}

}
