using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface IStrMutListEffect {
  int id { get; }
  void visit(IStrMutListEffectVisitor visitor);
}

}
