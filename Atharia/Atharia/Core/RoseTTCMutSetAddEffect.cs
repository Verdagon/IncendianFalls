using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RoseTTCMutSetAddEffect : IRoseTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RoseTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRoseTTCMutSetEffect.id => id;
  public void visitIRoseTTCMutSetEffect(IRoseTTCMutSetEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRoseTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
