using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct IncendianFallsLevelLinkerTTCMutSetRemoveEffect : IIncendianFallsLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public IncendianFallsLevelLinkerTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIncendianFallsLevelLinkerTTCMutSetEffect.id => id;
  public void visit(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetRemoveEffect(this);
  }
}

}
