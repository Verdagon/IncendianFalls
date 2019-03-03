using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeTerrainTileComponentMutSetEffectVisitor {
  void visitDecorativeTerrainTileComponentMutSetCreateEffect(DecorativeTerrainTileComponentMutSetCreateEffect effect);
  void visitDecorativeTerrainTileComponentMutSetDeleteEffect(DecorativeTerrainTileComponentMutSetDeleteEffect effect);
  void visitDecorativeTerrainTileComponentMutSetAddEffect(DecorativeTerrainTileComponentMutSetAddEffect effect);
  void visitDecorativeTerrainTileComponentMutSetRemoveEffect(DecorativeTerrainTileComponentMutSetRemoveEffect effect);
}
         
}
