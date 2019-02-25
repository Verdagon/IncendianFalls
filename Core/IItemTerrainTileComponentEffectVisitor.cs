using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTerrainTileComponentEffectVisitor {
  void visitItemTerrainTileComponentCreateEffect(ItemTerrainTileComponentCreateEffect effect);
  void visitItemTerrainTileComponentDeleteEffect(ItemTerrainTileComponentDeleteEffect effect);
}

}
