using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameSetOverlayEffect : IGameEffect {
  public readonly int id;
  public readonly Overlay newValue;
  public GameSetOverlayEffect(
      int id,
      Overlay newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IGameEffect.id => id;

  public void visit(IGameEffectVisitor visitor) {
    visitor.visitGameSetOverlayEffect(this);
  }
}

}
