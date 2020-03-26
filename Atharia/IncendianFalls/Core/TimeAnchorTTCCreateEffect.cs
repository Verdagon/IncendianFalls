using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TimeAnchorTTCCreateEffect : ITimeAnchorTTCEffect {
  public readonly int id;
  public readonly TimeAnchorTTCIncarnation incarnation;
  public TimeAnchorTTCCreateEffect(int id, TimeAnchorTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITimeAnchorTTCEffect.id => id;
  public void visitITimeAnchorTTCEffect(ITimeAnchorTTCEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTimeAnchorTTCEffect(this);
  }
}

}
