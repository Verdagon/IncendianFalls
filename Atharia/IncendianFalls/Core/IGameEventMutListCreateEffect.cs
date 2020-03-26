using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IGameEventMutListCreateEffect : IIGameEventMutListEffect {
  public readonly int id;
  public IGameEventMutListCreateEffect(int id) {
    this.id = id;
  }
  int IIGameEventMutListEffect.id => id;
  public void visitIIGameEventMutListEffect(IIGameEventMutListEffectVisitor visitor) {
    visitor.visitIGameEventMutListCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIGameEventMutListEffect(this);
  }
}

}
