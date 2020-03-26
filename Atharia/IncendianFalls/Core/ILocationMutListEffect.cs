using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILocationMutListEffect : IEffect {
  int id { get; }
  void visitILocationMutListEffect(ILocationMutListEffectVisitor visitor);
}

}
