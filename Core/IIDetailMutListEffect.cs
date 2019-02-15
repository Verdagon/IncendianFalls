using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIDetailMutListEffect {
  int id { get; }
  void visit(IIDetailMutListEffectVisitor visitor);
}

}
