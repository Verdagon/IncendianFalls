using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseTerrainTileComponentEffectVisitor {
  void visitDownStaircaseTerrainTileComponentCreateEffect(DownStaircaseTerrainTileComponentCreateEffect effect);
  void visitDownStaircaseTerrainTileComponentDeleteEffect(DownStaircaseTerrainTileComponentDeleteEffect effect);
}

}
