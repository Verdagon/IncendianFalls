using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RoseTTCIncarnation : IRoseTTCEffectVisitor {
  public RoseTTCIncarnation(
) {
  }
  public RoseTTCIncarnation Copy() {
    return new RoseTTCIncarnation(
    );
  }

  public void visitRoseTTCCreateEffect(RoseTTCCreateEffect e) {}
  public void visitRoseTTCDeleteEffect(RoseTTCDeleteEffect e) {}

  public void ApplyEffect(IRoseTTCEffect effect) { effect.visitIRoseTTCEffect(this); }
}

}
