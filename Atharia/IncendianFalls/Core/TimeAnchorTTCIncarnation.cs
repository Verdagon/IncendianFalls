using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeAnchorTTCIncarnation : ITimeAnchorTTCEffectVisitor {
  public readonly int pastVersion;
  public TimeAnchorTTCIncarnation(
      int pastVersion) {
    this.pastVersion = pastVersion;
  }
  public TimeAnchorTTCIncarnation Copy() {
    return new TimeAnchorTTCIncarnation(
pastVersion    );
  }

  public void visitTimeAnchorTTCCreateEffect(TimeAnchorTTCCreateEffect e) {}
  public void visitTimeAnchorTTCDeleteEffect(TimeAnchorTTCDeleteEffect e) {}

  public void ApplyEffect(ITimeAnchorTTCEffect effect) { effect.visitITimeAnchorTTCEffect(this); }
}

}
