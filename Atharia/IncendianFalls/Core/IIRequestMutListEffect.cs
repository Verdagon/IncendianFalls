using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIRequestMutListEffect : IEffect {
  int id { get; }
  void visitIIRequestMutListEffect(IIRequestMutListEffectVisitor visitor);
}

}
