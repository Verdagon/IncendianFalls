using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseTerrainTileComponentMutSetEffectVisitor {
  void visitUpStaircaseTerrainTileComponentMutSetCreateEffect(UpStaircaseTerrainTileComponentMutSetCreateEffect effect);
  void visitUpStaircaseTerrainTileComponentMutSetDeleteEffect(UpStaircaseTerrainTileComponentMutSetDeleteEffect effect);
  void visitUpStaircaseTerrainTileComponentMutSetAddEffect(UpStaircaseTerrainTileComponentMutSetAddEffect effect);
  void visitUpStaircaseTerrainTileComponentMutSetRemoveEffect(UpStaircaseTerrainTileComponentMutSetRemoveEffect effect);
}
         
}
