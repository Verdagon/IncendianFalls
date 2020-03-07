using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GuardAICapabilityUCIncarnation {
  public readonly Location guardCenterLocation;
  public readonly int guardRadius;
  public GuardAICapabilityUCIncarnation(
      Location guardCenterLocation,
      int guardRadius) {
    this.guardCenterLocation = guardCenterLocation;
    this.guardRadius = guardRadius;
  }
}

}
