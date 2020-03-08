using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeAICapabilityUCIncarnation {
  public  int targetByLocation;
  public  Location targetLocationCenter;
  public KamikazeAICapabilityUCIncarnation(
      int targetByLocation,
      Location targetLocationCenter) {
    this.targetByLocation = targetByLocation;
    this.targetLocationCenter = targetLocationCenter;
  }
}

}
