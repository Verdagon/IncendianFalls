using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelLinkTTCMutSetDeleteEffect : ILevelLinkTTCMutSetEffect {
  public readonly int id;
  public LevelLinkTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILevelLinkTTCMutSetEffect.id => id;
  public void visitILevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
