using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelLinkTTCMutSetRemoveEffect : ILevelLinkTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LevelLinkTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILevelLinkTTCMutSetEffect.id => id;
  public void visitILevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
