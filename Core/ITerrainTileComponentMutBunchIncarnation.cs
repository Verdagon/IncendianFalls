using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchIncarnation {
  public readonly int membersItemTerrainTileComponentMutSet;
  public readonly int membersDecorativeTerrainTileComponentMutSet;
  public readonly int membersUpStaircaseTerrainTileComponentMutSet;
  public readonly int membersDownStaircaseTerrainTileComponentMutSet;
  public ITerrainTileComponentMutBunchIncarnation(
      int membersItemTerrainTileComponentMutSet,
      int membersDecorativeTerrainTileComponentMutSet,
      int membersUpStaircaseTerrainTileComponentMutSet,
      int membersDownStaircaseTerrainTileComponentMutSet) {
    this.membersItemTerrainTileComponentMutSet = membersItemTerrainTileComponentMutSet;
    this.membersDecorativeTerrainTileComponentMutSet = membersDecorativeTerrainTileComponentMutSet;
    this.membersUpStaircaseTerrainTileComponentMutSet = membersUpStaircaseTerrainTileComponentMutSet;
    this.membersDownStaircaseTerrainTileComponentMutSet = membersDownStaircaseTerrainTileComponentMutSet;
  }
}

}
