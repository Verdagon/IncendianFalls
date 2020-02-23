using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainTileByLocationMutMapEffectVisitor {
  void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect);
  void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect);
  void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect);
  void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect);
}
         
}
