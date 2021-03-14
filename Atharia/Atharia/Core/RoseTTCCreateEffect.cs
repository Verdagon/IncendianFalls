using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RoseTTCCreateEffect : IRoseTTCEffect {
  public readonly int id;
  public readonly RoseTTCIncarnation incarnation;
  public RoseTTCCreateEffect(int id, RoseTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRoseTTCEffect.id => id;
  public void visitIRoseTTCEffect(IRoseTTCEffectVisitor visitor) {
    visitor.visitRoseTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRoseTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
