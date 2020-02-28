using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IncendianFallsLevelLinkerTTCCreateEffect : IIncendianFallsLevelLinkerTTCEffect {
  public readonly int id;
  public IncendianFallsLevelLinkerTTCCreateEffect(int id) {
    this.id = id;
  }
  int IIncendianFallsLevelLinkerTTCEffect.id => id;
  public void visit(IIncendianFallsLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCCreateEffect(this);
  }
}

}
