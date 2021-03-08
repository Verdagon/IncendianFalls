using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SpeedRingIncarnation : ISpeedRingEffectVisitor {
  public SpeedRingIncarnation(
) {
  }
  public SpeedRingIncarnation Copy() {
    return new SpeedRingIncarnation(
    );
  }

  public void visitSpeedRingCreateEffect(SpeedRingCreateEffect e) {}
  public void visitSpeedRingDeleteEffect(SpeedRingDeleteEffect e) {}

  public void ApplyEffect(ISpeedRingEffect effect) { effect.visitISpeedRingEffect(this); }
}

}
