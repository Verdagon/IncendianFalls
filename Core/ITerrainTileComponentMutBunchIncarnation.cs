using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchIncarnation {
  public readonly int membersTimeAnchorTTCMutSet;
  public readonly int membersItemTTCMutSet;
  public readonly int membersDecorativeTTCMutSet;
  public readonly int membersUpStaircaseTTCMutSet;
  public readonly int membersDownStaircaseTTCMutSet;
  public ITerrainTileComponentMutBunchIncarnation(
      int membersTimeAnchorTTCMutSet,
      int membersItemTTCMutSet,
      int membersDecorativeTTCMutSet,
      int membersUpStaircaseTTCMutSet,
      int membersDownStaircaseTTCMutSet) {
    this.membersTimeAnchorTTCMutSet = membersTimeAnchorTTCMutSet;
    this.membersItemTTCMutSet = membersItemTTCMutSet;
    this.membersDecorativeTTCMutSet = membersDecorativeTTCMutSet;
    this.membersUpStaircaseTTCMutSet = membersUpStaircaseTTCMutSet;
    this.membersDownStaircaseTTCMutSet = membersDownStaircaseTTCMutSet;
  }
}

}
