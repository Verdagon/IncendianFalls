using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetHideInputEffect : IGameEffect {
  public readonly int id;
  public readonly bool newValue;
  public GameSetHideInputEffect(
      int id,
      bool newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameSetHideInputEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
