using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IGameEventMutListDeleteEffect : IIGameEventMutListEffect {
  public readonly int id;
  public IGameEventMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IIGameEventMutListEffect.id => id;
  public void visitIIGameEventMutListEffect(IIGameEventMutListEffectVisitor visitor) {
    visitor.visitIGameEventMutListDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIGameEventMutListEffect(this);
  }
}

}
