using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct IncendianFallsLevelLinkerTTCMutSetDeleteEffect : IIncendianFallsLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public IncendianFallsLevelLinkerTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IIncendianFallsLevelLinkerTTCMutSetEffect.id => id;
  public void visitIIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
