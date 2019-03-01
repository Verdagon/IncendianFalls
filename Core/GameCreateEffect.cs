using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameCreateEffect : IGameEffect {
  public readonly int id;
  public GameCreateEffect(int id) {
    this.id = id;
  }
  int IGameEffect.id => id;
  public void visit(IGameEffectVisitor visitor) {
    visitor.visitGameCreateEffect(this);
  }
}

}
