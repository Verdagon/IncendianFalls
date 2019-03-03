using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTerrainTileComponentMutSetEffectVisitor {
  void visitItemTerrainTileComponentMutSetCreateEffect(ItemTerrainTileComponentMutSetCreateEffect effect);
  void visitItemTerrainTileComponentMutSetDeleteEffect(ItemTerrainTileComponentMutSetDeleteEffect effect);
  void visitItemTerrainTileComponentMutSetAddEffect(ItemTerrainTileComponentMutSetAddEffect effect);
  void visitItemTerrainTileComponentMutSetRemoveEffect(ItemTerrainTileComponentMutSetRemoveEffect effect);
}
         
}
