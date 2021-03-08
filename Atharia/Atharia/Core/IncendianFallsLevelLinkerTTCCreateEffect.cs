using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IncendianFallsLevelLinkerTTCCreateEffect : IIncendianFallsLevelLinkerTTCEffect {
  public readonly int id;
  public readonly IncendianFallsLevelLinkerTTCIncarnation incarnation;
  public IncendianFallsLevelLinkerTTCCreateEffect(int id, IncendianFallsLevelLinkerTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIncendianFallsLevelLinkerTTCEffect.id => id;
  public void visitIIncendianFallsLevelLinkerTTCEffect(IIncendianFallsLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIncendianFallsLevelLinkerTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
