using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetInstructionsEffect : IGameEffect {
  public readonly int id;
  public readonly string newValue;
  public GameSetInstructionsEffect(
      int id,
      string newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameSetInstructionsEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
