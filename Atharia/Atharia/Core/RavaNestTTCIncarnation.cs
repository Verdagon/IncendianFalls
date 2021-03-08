using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaNestTTCIncarnation : IRavaNestTTCEffectVisitor {
  public RavaNestTTCIncarnation(
) {
  }
  public RavaNestTTCIncarnation Copy() {
    return new RavaNestTTCIncarnation(
    );
  }

  public void visitRavaNestTTCCreateEffect(RavaNestTTCCreateEffect e) {}
  public void visitRavaNestTTCDeleteEffect(RavaNestTTCDeleteEffect e) {}

  public void ApplyEffect(IRavaNestTTCEffect effect) { effect.visitIRavaNestTTCEffect(this); }
}

}
