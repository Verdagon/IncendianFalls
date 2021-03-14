using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireTTCIncarnation : IOnFireTTCEffectVisitor {
  public  int turnsRemaining;
  public OnFireTTCIncarnation(
      int turnsRemaining) {
    this.turnsRemaining = turnsRemaining;
  }
  public OnFireTTCIncarnation Copy() {
    return new OnFireTTCIncarnation(
turnsRemaining    );
  }

  public void visitOnFireTTCCreateEffect(OnFireTTCCreateEffect e) {}
  public void visitOnFireTTCDeleteEffect(OnFireTTCDeleteEffect e) {}
public void visitOnFireTTCSetTurnsRemainingEffect(OnFireTTCSetTurnsRemainingEffect e) { this.turnsRemaining = e.newValue; }
  public void ApplyEffect(IOnFireTTCEffect effect) { effect.visitIOnFireTTCEffect(this); }
}

}
