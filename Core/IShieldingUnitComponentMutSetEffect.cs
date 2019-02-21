using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUnitComponentMutSetEffect {
  int id { get; }
  void visit(IShieldingUnitComponentMutSetEffectVisitor visitor);
}

}
