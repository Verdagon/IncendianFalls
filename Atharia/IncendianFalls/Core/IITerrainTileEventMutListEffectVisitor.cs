using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IITerrainTileEventMutListEffectVisitor {
  void visitITerrainTileEventMutListCreateEffect(ITerrainTileEventMutListCreateEffect effect);
  void visitITerrainTileEventMutListDeleteEffect(ITerrainTileEventMutListDeleteEffect effect);
  void visitITerrainTileEventMutListAddEffect(ITerrainTileEventMutListAddEffect effect);
  void visitITerrainTileEventMutListRemoveEffect(ITerrainTileEventMutListRemoveEffect effect);
}
         
}
