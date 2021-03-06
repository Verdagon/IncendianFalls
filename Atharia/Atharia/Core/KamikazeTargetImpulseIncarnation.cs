using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetImpulseIncarnation : IKamikazeTargetImpulseEffectVisitor {
  public readonly int weight;
  public readonly int capability;
  public readonly Location targetLocationCenter;
  public readonly LocationImmList targetLocations;
  public KamikazeTargetImpulseIncarnation(
      int weight,
      int capability,
      Location targetLocationCenter,
      LocationImmList targetLocations) {
    this.weight = weight;
    this.capability = capability;
    this.targetLocationCenter = targetLocationCenter;
    this.targetLocations = targetLocations;
  }
  public KamikazeTargetImpulseIncarnation Copy() {
    return new KamikazeTargetImpulseIncarnation(
weight,
capability,
targetLocationCenter,
targetLocations    );
  }

  public void visitKamikazeTargetImpulseCreateEffect(KamikazeTargetImpulseCreateEffect e) {}
  public void visitKamikazeTargetImpulseDeleteEffect(KamikazeTargetImpulseDeleteEffect e) {}




  public void ApplyEffect(IKamikazeTargetImpulseEffect effect) { effect.visitIKamikazeTargetImpulseEffect(this); }
}

}
