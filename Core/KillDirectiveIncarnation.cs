using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KillDirectiveIncarnation {
  public readonly int targetUnit;
  public readonly int pathToLastSeenLocation;
  public KillDirectiveIncarnation(
      int targetUnit,
      int pathToLastSeenLocation) {
    this.targetUnit = targetUnit;
    this.pathToLastSeenLocation = pathToLastSeenLocation;
  }
}

}
