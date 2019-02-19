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
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + targetUnit.GetDeterministicHashCode();
    s = s * 37 + pathToLastSeenLocation.GetDeterministicHashCode();
    return s;
  }
}

}
