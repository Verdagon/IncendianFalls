using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StaircaseTTCIncarnation {
  public readonly int portalIndex;
  public  int destinationLevel;
  public  int destinationLevelPortalIndex;
  public StaircaseTTCIncarnation(
      int portalIndex,
      int destinationLevel,
      int destinationLevelPortalIndex) {
    this.portalIndex = portalIndex;
    this.destinationLevel = destinationLevel;
    this.destinationLevelPortalIndex = destinationLevelPortalIndex;
  }
}

}
