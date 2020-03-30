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
  public void visitITimeAnchorTTCMutSetEffect(ITimeAnchorTTCMutSetEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
