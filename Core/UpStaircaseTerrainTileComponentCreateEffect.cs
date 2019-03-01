using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseTerrainTileComponentCreateEffect : IUpStaircaseTerrainTileComponentEffect {
  public readonly int id;
  public UpStaircaseTerrainTileComponentCreateEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseTerrainTileComponentEffect.id => id;
  public void visit(IUpStaircaseTerrainTileComponentEffectVisitor visitor) {
    visitor.visitUpStaircaseTerrainTileComponentCreateEffect(this);
  }
}

}
