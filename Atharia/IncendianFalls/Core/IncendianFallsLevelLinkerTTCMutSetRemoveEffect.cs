using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct IncendianFallsLevelLinkerTTCMutSetRemoveEffect : IIncendianFallsLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public IncendianFallsLevelLinkerTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IIncendianFallsLevelLinkerTTCMutSetEffect.id => id;
  public void visitIIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetEffect(this);
  }
}

}
