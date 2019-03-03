using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeTerrainTileComponentEffectVisitor {
  void visitDecorativeTerrainTileComponentCreateEffect(DecorativeTerrainTileComponentCreateEffect effect);
  void visitDecorativeTerrainTileComponentDeleteEffect(DecorativeTerrainTileComponentDeleteEffect effect);
}

}
