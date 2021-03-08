using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FloorTTCIncarnation : IFloorTTCEffectVisitor {
  public FloorTTCIncarnation(
) {
  }
  public FloorTTCIncarnation Copy() {
    return new FloorTTCIncarnation(
    );
  }

  public void visitFloorTTCCreateEffect(FloorTTCCreateEffect e) {}
  public void visitFloorTTCDeleteEffect(FloorTTCDeleteEffect e) {}

  public void ApplyEffect(IFloorTTCEffect effect) { effect.visitIFloorTTCEffect(this); }
}

}
