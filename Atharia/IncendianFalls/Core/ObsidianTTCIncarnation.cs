using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ObsidianTTCIncarnation : IObsidianTTCEffectVisitor {
  public ObsidianTTCIncarnation(
) {
  }
  public ObsidianTTCIncarnation Copy() {
    return new ObsidianTTCIncarnation(
    );
  }

  public void visitObsidianTTCCreateEffect(ObsidianTTCCreateEffect e) {}
  public void visitObsidianTTCDeleteEffect(ObsidianTTCDeleteEffect e) {}

  public void ApplyEffect(IObsidianTTCEffect effect) { effect.visitIObsidianTTCEffect(this); }
}

}
