using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchIncarnation {
  public readonly int membersTimeAnchorTTCMutSet;
  public readonly int membersStaircaseTTCMutSet;
  public readonly int membersItemTTCMutSet;
  public readonly int membersDecorativeTTCMutSet;
  public ITerrainTileComponentMutBunchIncarnation(
      int membersTimeAnchorTTCMutSet,
      int membersStaircaseTTCMutSet,
      int membersItemTTCMutSet,
      int membersDecorativeTTCMutSet) {
    this.membersTimeAnchorTTCMutSet = membersTimeAnchorTTCMutSet;
    this.membersStaircaseTTCMutSet = membersStaircaseTTCMutSet;
    this.membersItemTTCMutSet = membersItemTTCMutSet;
    this.membersDecorativeTTCMutSet = membersDecorativeTTCMutSet;
  }
}

}
