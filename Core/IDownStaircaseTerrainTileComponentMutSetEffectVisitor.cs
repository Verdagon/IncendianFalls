using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseTerrainTileComponentMutSetEffectVisitor {
  void visitDownStaircaseTerrainTileComponentMutSetCreateEffect(DownStaircaseTerrainTileComponentMutSetCreateEffect effect);
  void visitDownStaircaseTerrainTileComponentMutSetDeleteEffect(DownStaircaseTerrainTileComponentMutSetDeleteEffect effect);
  void visitDownStaircaseTerrainTileComponentMutSetAddEffect(DownStaircaseTerrainTileComponentMutSetAddEffect effect);
  void visitDownStaircaseTerrainTileComponentMutSetRemoveEffect(DownStaircaseTerrainTileComponentMutSetRemoveEffect effect);
}
         
}
