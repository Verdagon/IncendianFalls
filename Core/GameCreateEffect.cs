using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct GameCreateEffect : IGameEffect {
  public readonly int id;
  public readonly GameIncarnation incarnation;
  public GameCreateEffect(
      int id,
      GameIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IGameEffect.id => id;
  public void visit(IGameEffectVisitor visitor) {
    visitor.visitGameCreateEffect(this);
  }
}
       
}
