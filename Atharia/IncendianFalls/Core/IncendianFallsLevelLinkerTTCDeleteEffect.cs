using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IncendianFallsLevelLinkerTTCDeleteEffect : IIncendianFallsLevelLinkerTTCEffect {
  public readonly int id;
  public IncendianFallsLevelLinkerTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IIncendianFallsLevelLinkerTTCEffect.id => id;
  public void visitIIncendianFallsLevelLinkerTTCEffect(IIncendianFallsLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCEffect(this);
  }
}

}
