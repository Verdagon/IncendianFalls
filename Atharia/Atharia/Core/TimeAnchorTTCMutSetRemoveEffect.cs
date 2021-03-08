using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeAnchorTTCMutSetRemoveEffect : ITimeAnchorTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TimeAnchorTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITimeAnchorTTCMutSetEffect.id => id;
  public void visitITimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
