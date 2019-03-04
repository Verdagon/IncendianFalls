using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeAnchorTTCMutSetCreateEffect : ITimeAnchorTTCMutSetEffect {
  public readonly int id;
  public TimeAnchorTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITimeAnchorTTCMutSetEffect.id => id;
  public void visit(ITimeAnchorTTCMutSetEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetCreateEffect(this);
  }
}

}
