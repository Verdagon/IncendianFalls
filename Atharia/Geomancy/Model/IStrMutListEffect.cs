using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface IStrMutListEffect : IEffect {
  int id { get; }
  void visitIStrMutListEffect(IStrMutListEffectVisitor visitor);
}

}
