using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RoseTTCMutSetCreateEffect : IRoseTTCMutSetEffect {
  public readonly int id;
  public RoseTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IRoseTTCMutSetEffect.id => id;
  public void visitIRoseTTCMutSetEffect(IRoseTTCMutSetEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
