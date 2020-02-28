using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelLinkTTCDeleteEffect : ILevelLinkTTCEffect {
  public readonly int id;
  public LevelLinkTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ILevelLinkTTCEffect.id => id;
  public void visit(ILevelLinkTTCEffectVisitor visitor) {
    visitor.visitLevelLinkTTCDeleteEffect(this);
  }
}

}
