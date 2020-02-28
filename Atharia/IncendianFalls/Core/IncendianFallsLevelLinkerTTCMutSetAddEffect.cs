using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct IncendianFallsLevelLinkerTTCMutSetAddEffect : IIncendianFallsLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public IncendianFallsLevelLinkerTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIncendianFallsLevelLinkerTTCMutSetEffect.id => id;
  public void visit(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetAddEffect(this);
  }
}

}
