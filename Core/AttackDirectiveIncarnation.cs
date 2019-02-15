using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class AttackDirectiveIncarnation {
  public int targetUnit;
  public int pathToLastSeenLocation;
  public AttackDirectiveIncarnation(
      int targetUnit,
      int pathToLastSeenLocation) {
    this.targetUnit = targetUnit;
    this.pathToLastSeenLocation = pathToLastSeenLocation;
  }
}

}
