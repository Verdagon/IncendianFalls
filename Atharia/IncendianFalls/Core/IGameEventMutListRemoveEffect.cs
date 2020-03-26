using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IGameEventMutListRemoveEffect : IIGameEventMutListEffect {
  public readonly int id;
  public readonly int index;
  public IGameEventMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int IIGameEventMutListEffect.id => id;
  public void visitIIGameEventMutListEffect(IIGameEventMutListEffectVisitor visitor) {
    visitor.visitIGameEventMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIGameEventMutListEffect(this);
  }
}

}
