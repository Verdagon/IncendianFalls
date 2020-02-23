using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetPlayerEffect : IGameEffect {
  public readonly int id;
  public readonly Unit newValue;
  public GameSetPlayerEffect(
      int id,
      Unit newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visit(IGameEffectVisitor visitor) {
    visitor.visitGameSetPlayerEffect(this);
  }
}

}
