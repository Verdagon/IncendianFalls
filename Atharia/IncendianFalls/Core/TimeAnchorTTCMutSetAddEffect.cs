using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeAnchorTTCMutSetAddEffect : ITimeAnchorTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TimeAnchorTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITimeAnchorTTCMutSetEffect.id => id;
  public void visitITimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetEffect(this);
  }
}

}
