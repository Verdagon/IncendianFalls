using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RocksTTCMutSetAddEffect : IRocksTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public RocksTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IRocksTTCMutSetEffect.id => id;
  public void visit(IRocksTTCMutSetEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetAddEffect(this);
  }
}

}
