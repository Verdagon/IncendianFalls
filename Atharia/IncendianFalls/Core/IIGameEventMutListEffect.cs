using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIGameEventMutListEffect : IEffect {
  int id { get; }
  void visitIIGameEventMutListEffect(IIGameEventMutListEffectVisitor visitor);
}

}
