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
  public void visitILevelLinkTTCEffect(ILevelLinkTTCEffectVisitor visitor) {
    visitor.visitLevelLinkTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelLinkTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
