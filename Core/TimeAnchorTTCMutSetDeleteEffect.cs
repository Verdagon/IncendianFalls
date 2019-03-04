using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TimeAnchorTTCMutSetDeleteEffect : ITimeAnchorTTCMutSetEffect {
  public readonly int id;
  public TimeAnchorTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeAnchorTTCMutSetEffect.id => id;
  public void visit(ITimeAnchorTTCMutSetEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetDeleteEffect(this);
  }
}

}
