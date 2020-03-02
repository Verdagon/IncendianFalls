using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIGameEventMutListEffect {
  int id { get; }
  void visit(IIGameEventMutListEffectVisitor visitor);
}

}
