using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WarperTTCIncarnation : IWarperTTCEffectVisitor {
  public readonly Location destinationLocation;
  public WarperTTCIncarnation(
      Location destinationLocation) {
    this.destinationLocation = destinationLocation;
  }
  public WarperTTCIncarnation Copy() {
    return new WarperTTCIncarnation(
destinationLocation    );
  }

  public void visitWarperTTCCreateEffect(WarperTTCCreateEffect e) {}
  public void visitWarperTTCDeleteEffect(WarperTTCDeleteEffect e) {}

  public void ApplyEffect(IWarperTTCEffect effect) { effect.visitIWarperTTCEffect(this); }
}

}
