using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RocksTTCMutSetAddEffect : IRocksTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RocksTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRocksTTCMutSetEffect.id => id;
  public void visitIRocksTTCMutSetEffect(IRocksTTCMutSetEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetEffect(this);
  }
}

}
