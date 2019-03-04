using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIRequestMutListEffect {
  int id { get; }
  void visit(IIRequestMutListEffectVisitor visitor);
}

}
