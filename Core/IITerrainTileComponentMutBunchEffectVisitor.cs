using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IITerrainTileComponentMutBunchEffectVisitor {
  void visitITerrainTileComponentMutBunchCreateEffect(ITerrainTileComponentMutBunchCreateEffect effect);
  void visitITerrainTileComponentMutBunchDeleteEffect(ITerrainTileComponentMutBunchDeleteEffect effect);
}

}
