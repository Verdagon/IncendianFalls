using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainTileEffectVisitor {
  void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect);
  void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect);
  void visitTerrainTileSetEvventEffect(TerrainTileSetEvventEffect effect);
  void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect);
}

}
