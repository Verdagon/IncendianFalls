using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeAnchorTTCMutSetRemoveEffect : ITimeAnchorTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TimeAnchorTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITimeAnchorTTCMutSetEffect.id => id;
  public void visit(ITimeAnchorTTCMutSetEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetRemoveEffect(this);
  }
}

}
