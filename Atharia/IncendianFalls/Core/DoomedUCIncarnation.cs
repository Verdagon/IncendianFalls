using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DoomedUCIncarnation : IDoomedUCEffectVisitor {
  public readonly int deathTime;
  public DoomedUCIncarnation(
      int deathTime) {
    this.deathTime = deathTime;
  }
  public DoomedUCIncarnation Copy() {
    return new DoomedUCIncarnation(
deathTime    );
  }

  public void visitDoomedUCCreateEffect(DoomedUCCreateEffect e) {}
  public void visitDoomedUCDeleteEffect(DoomedUCDeleteEffect e) {}

  public void ApplyEffect(IDoomedUCEffect effect) { effect.visitIDoomedUCEffect(this); }
}

}
