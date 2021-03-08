using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetLevelEffect : IGameEffect {
  public readonly int id;
  public readonly int newValue;
  public GameSetLevelEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameSetLevelEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
