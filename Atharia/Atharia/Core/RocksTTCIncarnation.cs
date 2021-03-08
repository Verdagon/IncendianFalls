using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RocksTTCIncarnation : IRocksTTCEffectVisitor {
  public RocksTTCIncarnation(
) {
  }
  public RocksTTCIncarnation Copy() {
    return new RocksTTCIncarnation(
    );
  }

  public void visitRocksTTCCreateEffect(RocksTTCCreateEffect e) {}
  public void visitRocksTTCDeleteEffect(RocksTTCDeleteEffect e) {}

  public void ApplyEffect(IRocksTTCEffect effect) { effect.visitIRocksTTCEffect(this); }
}

}
