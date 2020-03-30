using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct IncendianFallsLevelLinkerTTCMutSetAddEffect : IIncendianFallsLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public IncendianFallsLevelLinkerTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IIncendianFallsLevelLinkerTTCMutSetEffect.id => id;
  public void visitIIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
