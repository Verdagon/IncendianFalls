using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IGameEventMutListAddEffect : IIGameEventMutListEffect {
  public readonly int id;
  public readonly IGameEvent element;
  public IGameEventMutListAddEffect(int id, IGameEvent element) {
    this.id = id;
    this.element = element;
  }
  int IIGameEventMutListEffect.id => id;
  public void visit(IIGameEventMutListEffectVisitor visitor) {
    visitor.visitIGameEventMutListAddEffect(this);
  }
}

}
