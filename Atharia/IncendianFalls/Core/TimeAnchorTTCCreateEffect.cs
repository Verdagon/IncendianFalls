using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeAnchorTTCCreateEffect : ITimeAnchorTTCEffect {
  public readonly int id;
  public TimeAnchorTTCCreateEffect(int id) {
    this.id = id;
  }
  int ITimeAnchorTTCEffect.id => id;
  public void visit(ITimeAnchorTTCEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCCreateEffect(this);
  }
}

}
