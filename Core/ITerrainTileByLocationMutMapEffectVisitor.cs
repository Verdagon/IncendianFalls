using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileByLocationMutMapEffectVisitor {
  void visitTerrainTileByLocationMutMapCreateEffect(TerrainTileByLocationMutMapCreateEffect effect);
  void visitTerrainTileByLocationMutMapDeleteEffect(TerrainTileByLocationMutMapDeleteEffect effect);
  void visitTerrainTileByLocationMutMapAddEffect(TerrainTileByLocationMutMapAddEffect effect);
  void visitTerrainTileByLocationMutMapRemoveEffect(TerrainTileByLocationMutMapRemoveEffect effect);
}
         
}
