using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianFloorTTCIncarnation : IObsidianFloorTTCEffectVisitor {
  public ObsidianFloorTTCIncarnation(
) {
  }
  public ObsidianFloorTTCIncarnation Copy() {
    return new ObsidianFloorTTCIncarnation(
    );
  }

  public void visitObsidianFloorTTCCreateEffect(ObsidianFloorTTCCreateEffect e) {}
  public void visitObsidianFloorTTCDeleteEffect(ObsidianFloorTTCDeleteEffect e) {}

  public void ApplyEffect(IObsidianFloorTTCEffect effect) { effect.visitIObsidianFloorTTCEffect(this); }
}

}
