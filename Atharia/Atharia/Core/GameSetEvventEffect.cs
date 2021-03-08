using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetEvventEffect : IGameEffect {
  public readonly int id;
  public readonly IGameEvent newValue;
  public GameSetEvventEffect(
      int id,
      IGameEvent newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameSetEvventEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
