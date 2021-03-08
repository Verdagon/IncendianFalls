using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICommMutListEffect : IEffect {
  int id { get; }
  void visitICommMutListEffect(ICommMutListEffectVisitor visitor);
}

}
