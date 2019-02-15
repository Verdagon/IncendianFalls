using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ILocationMutListEffect {
  int id { get; }
  void visit(ILocationMutListEffectVisitor visitor);
}

}
