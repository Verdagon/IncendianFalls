using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetActingUnitEffect : IGameEffect {
  public readonly int id;
  public readonly int newValue;
  public GameSetActingUnitEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameSetActingUnitEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
