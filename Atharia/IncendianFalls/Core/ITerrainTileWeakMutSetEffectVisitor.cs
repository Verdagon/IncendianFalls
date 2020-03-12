using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainTileWeakMutSetEffectVisitor {
  void visitTerrainTileWeakMutSetCreateEffect(TerrainTileWeakMutSetCreateEffect effect);
  void visitTerrainTileWeakMutSetDeleteEffect(TerrainTileWeakMutSetDeleteEffect effect);
  void visitTerrainTileWeakMutSetAddEffect(TerrainTileWeakMutSetAddEffect effect);
  void visitTerrainTileWeakMutSetRemoveEffect(TerrainTileWeakMutSetRemoveEffect effect);
}
         
}
