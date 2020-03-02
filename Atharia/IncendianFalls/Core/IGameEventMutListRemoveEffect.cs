using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IGameEventMutListRemoveEffect : IIGameEventMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public IGameEventMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIGameEventMutListEffect.id => id;
  public void visit(IIGameEventMutListEffectVisitor visitor) {
    visitor.visitIGameEventMutListRemoveEffect(this);
  }
}

}
