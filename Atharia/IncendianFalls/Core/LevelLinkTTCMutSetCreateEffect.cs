using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelLinkTTCMutSetCreateEffect : ILevelLinkTTCMutSetEffect {
  public readonly int id;
  public LevelLinkTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILevelLinkTTCMutSetEffect.id => id;
  public void visitILevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
