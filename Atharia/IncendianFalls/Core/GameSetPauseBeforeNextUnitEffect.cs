using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetPauseBeforeNextUnitEffect : IGameEffect {
  public readonly int id;
  public readonly bool newValue;
  public GameSetPauseBeforeNextUnitEffect(
      int id,
      bool newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameSetPauseBeforeNextUnitEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
}

}
