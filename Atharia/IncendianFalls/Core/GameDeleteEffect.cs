using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GameDeleteEffect : IGameEffect {
  public readonly int id;
  public GameDeleteEffect(int id) {
    this.id = id;
  }
  int IGameEffect.id => id;
  public void visitIGameEffect(IGameEffectVisitor visitor) {
    visitor.visitGameDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGameEffect(this);
  }
}

}
