using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class ITerrainTileComponentMutBunchIncarnation {
  public readonly int membersDecorativeTerrainTileComponentMutSet;
  public readonly int membersUpStaircaseTerrainTileComponentMutSet;
  public readonly int membersDownStaircaseTerrainTileComponentMutSet;
  public ITerrainTileComponentMutBunchIncarnation(
      int membersDecorativeTerrainTileComponentMutSet,
      int membersUpStaircaseTerrainTileComponentMutSet,
      int membersDownStaircaseTerrainTileComponentMutSet) {
    this.membersDecorativeTerrainTileComponentMutSet = membersDecorativeTerrainTileComponentMutSet;
    this.membersUpStaircaseTerrainTileComponentMutSet = membersUpStaircaseTerrainTileComponentMutSet;
    this.membersDownStaircaseTerrainTileComponentMutSet = membersDownStaircaseTerrainTileComponentMutSet;
  }
}

}
