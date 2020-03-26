using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IGameEventMutListAddEffect : IIGameEventMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly IGameEvent element;
  public IGameEventMutListAddEffect(int id, int index, IGameEvent element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int IIGameEventMutListEffect.id => id;
  public void visitIIGameEventMutListEffect(IIGameEventMutListEffectVisitor visitor) {
    visitor.visitIGameEventMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIGameEventMutListEffect(this);
  }
}

}
