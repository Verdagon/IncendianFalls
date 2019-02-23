using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KillDirectiveUCIncarnation {
  public readonly int targetUnit;
  public readonly int pathToLastSeenLocation;
  public KillDirectiveUCIncarnation(
      int targetUnit,
      int pathToLastSeenLocation) {
    this.targetUnit = targetUnit;
    this.pathToLastSeenLocation = pathToLastSeenLocation;
  }
}

}
