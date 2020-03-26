using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RocksTTCCreateEffect : IRocksTTCEffect {
  public readonly int id;
  public readonly RocksTTCIncarnation incarnation;
  public RocksTTCCreateEffect(int id, RocksTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRocksTTCEffect.id => id;
  public void visitIRocksTTCEffect(IRocksTTCEffectVisitor visitor) {
    visitor.visitRocksTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRocksTTCEffect(this);
  }
}

}
