using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RoseTTCDeleteEffect : IRoseTTCEffect {
  public readonly int id;
  public RoseTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IRoseTTCEffect.id => id;
  public void visitIRoseTTCEffect(IRoseTTCEffectVisitor visitor) {
    visitor.visitRoseTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRoseTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
