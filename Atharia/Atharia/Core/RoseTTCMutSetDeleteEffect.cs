using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RoseTTCMutSetDeleteEffect : IRoseTTCMutSetEffect {
  public readonly int id;
  public RoseTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IRoseTTCMutSetEffect.id => id;
  public void visitIRoseTTCMutSetEffect(IRoseTTCMutSetEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
