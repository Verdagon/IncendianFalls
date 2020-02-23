using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public interface ITerrainTileEffectVisitor {
  void visitTerrainTileCreateEffect(TerrainTileCreateEffect effect);
  void visitTerrainTileDeleteEffect(TerrainTileDeleteEffect effect);
  void visitTerrainTileSetElevationEffect(TerrainTileSetElevationEffect effect);
}

}
