using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeAnchorTTCDeleteEffect : ITimeAnchorTTCEffect {
  public readonly int id;
  public TimeAnchorTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ITimeAnchorTTCEffect.id => id;
  public void visit(ITimeAnchorTTCEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCDeleteEffect(this);
  }
}

}
