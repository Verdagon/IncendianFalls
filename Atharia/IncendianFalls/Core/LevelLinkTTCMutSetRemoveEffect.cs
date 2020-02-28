using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelLinkTTCMutSetRemoveEffect : ILevelLinkTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LevelLinkTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILevelLinkTTCMutSetEffect.id => id;
  public void visit(ILevelLinkTTCMutSetEffectVisitor visitor) {
    visitor.visitLevelLinkTTCMutSetRemoveEffect(this);
  }
}

}
