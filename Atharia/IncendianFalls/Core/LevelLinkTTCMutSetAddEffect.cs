using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelLinkTTCMutSetAddEffect : ILevelLinkTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LevelLinkTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILevelLinkTTCMutSetEffect.id => id;
  public void visitILevelLinkTTCMutSetEffect(ILevelLinkTTCMutSetEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetEffect(this);
  }
}

}
