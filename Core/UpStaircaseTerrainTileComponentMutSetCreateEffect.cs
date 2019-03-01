using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpStaircaseTerrainTileComponentMutSetCreateEffect : IUpStaircaseTerrainTileComponentMutSetEffect {
  public readonly int id;
  public UpStaircaseTerrainTileComponentMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTerrainTileComponentMutSetEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentMutSetEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentMutSetCreateEffect(this);
  }
}

}
