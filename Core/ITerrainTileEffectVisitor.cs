using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainTileEffectVisitor {
  void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect);
  void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect);
  void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect);
  void visitTerrainTileSetClassIdEffect(TerrainTileSetClassIdEffect effect);
}

}
