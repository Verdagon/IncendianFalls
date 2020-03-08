using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeJumpImpulseIncarnation {
  public readonly int weight;
  public readonly int capability;
  public readonly Location jumpTarget;
  public KamikazeJumpImpulseIncarnation(
      int weight,
      int capability,
      Location jumpTarget) {
    this.weight = weight;
    this.capability = capability;
    this.jumpTarget = jumpTarget;
  }
}

}
