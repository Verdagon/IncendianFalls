using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelLinkTTCIncarnation {
  public readonly int destinationLevel;
  public readonly Location destinationLevelLocation;
  public LevelLinkTTCIncarnation(
      int destinationLevel,
      Location destinationLevelLocation) {
    this.destinationLevel = destinationLevel;
    this.destinationLevelLocation = destinationLevelLocation;
  }
}

}
