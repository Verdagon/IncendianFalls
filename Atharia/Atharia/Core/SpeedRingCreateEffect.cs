using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SpeedRingCreateEffect : ISpeedRingEffect {
  public readonly int id;
  public readonly SpeedRingIncarnation incarnation;
  public SpeedRingCreateEffect(int id, SpeedRingIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISpeedRingEffect.id => id;
  public void visitISpeedRingEffect(ISpeedRingEffectVisitor visitor) {
    visitor.visitSpeedRingCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
