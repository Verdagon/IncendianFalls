using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RocksTTCMutSetRemoveEffect : IRocksTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RocksTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRocksTTCMutSetEffect.id => id;
  public void visitIRocksTTCMutSetEffect(IRocksTTCMutSetEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetEffect(this);
  }
}

}
