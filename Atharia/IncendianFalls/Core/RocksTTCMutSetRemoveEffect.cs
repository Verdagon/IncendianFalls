using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RocksTTCMutSetRemoveEffect : IRocksTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public RocksTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IRocksTTCMutSetEffect.id => id;
  public void visit(IRocksTTCMutSetEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetRemoveEffect(this);
  }
}

}
