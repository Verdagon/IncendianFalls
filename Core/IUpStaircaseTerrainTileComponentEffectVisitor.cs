using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseTerrainTileComponentEffectVisitor {
  void visitUpStaircaseTerrainTileComponentCreateEffect(UpStaircaseTerrainTileComponentCreateEffect effect);
  void visitUpStaircaseTerrainTileComponentDeleteEffect(UpStaircaseTerrainTileComponentDeleteEffect effect);
}

}
