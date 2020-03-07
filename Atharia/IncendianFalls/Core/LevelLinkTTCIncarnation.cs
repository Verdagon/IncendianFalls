using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelLinkTTCIncarnation {
  public readonly bool destroyThisLevel;
  public readonly int destinationLevel;
  public readonly Location destinationLevelLocation;
  public LevelLinkTTCIncarnation(
      bool destroyThisLevel,
      int destinationLevel,
      Location destinationLevelLocation) {
    this.destroyThisLevel = destroyThisLevel;
    this.destinationLevel = destinationLevel;
    this.destinationLevelLocation = destinationLevelLocation;
  }
}

}
