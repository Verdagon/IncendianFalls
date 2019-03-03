using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetLastPlayerRequestEffect : IGameEffect {
  public readonly int id;
  public readonly IRequest newValue;
  public GameSetLastPlayerRequestEffect(
      int id,
      IRequest newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visit(IGameEffectVisitor visitor) {
    visitor.visitGameSetLastPlayerRequestEffect(this);
  }
}

}
