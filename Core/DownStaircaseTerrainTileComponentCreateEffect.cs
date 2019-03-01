using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseTerrainTileComponentCreateEffect : IDownStaircaseTerrainTileComponentEffect {
  public readonly int id;
  public DownStaircaseTerrainTileComponentCreateEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTerrainTileComponentEffect.id => id;
  public void visit(IDownStaircaseTerrainTileComponentEffectVisitor visitor) {
    visitor.visitDownStaircaseTerrainTileComponentCreateEffect(this);
  }
}

}
