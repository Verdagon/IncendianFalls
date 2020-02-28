using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct IncendianFallsLevelLinkerTTCMutSetCreateEffect : IIncendianFallsLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public IncendianFallsLevelLinkerTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IIncendianFallsLevelLinkerTTCMutSetEffect.id => id;
  public void visit(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetCreateEffect(this);
  }
}

}
