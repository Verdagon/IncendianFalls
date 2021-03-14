using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RoseTTCMutSetRemoveEffect : IRoseTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RoseTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRoseTTCMutSetEffect.id => id;
  public void visitIRoseTTCMutSetEffect(IRoseTTCMutSetEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
