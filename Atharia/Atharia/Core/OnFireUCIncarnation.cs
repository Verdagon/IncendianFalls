using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OnFireUCIncarnation : IOnFireUCEffectVisitor {
  public readonly int turnsRemaining;
  public OnFireUCIncarnation(
      int turnsRemaining) {
    this.turnsRemaining = turnsRemaining;
  }
  public OnFireUCIncarnation Copy() {
    return new OnFireUCIncarnation(
turnsRemaining    );
  }

  public void visitOnFireUCCreateEffect(OnFireUCCreateEffect e) {}
  public void visitOnFireUCDeleteEffect(OnFireUCDeleteEffect e) {}

  public void ApplyEffect(IOnFireUCEffect effect) { effect.visitIOnFireUCEffect(this); }
}

}
