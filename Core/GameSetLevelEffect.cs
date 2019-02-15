using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetLevelEffect : IGameEffect {
  public readonly int id;
  public readonly Level newValue;
  public GameSetLevelEffect(
      int id,
      Level newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visit(IGameEffectVisitor visitor) {
    visitor.visitGameSetLevelEffect(this);
  }
}
           
}
