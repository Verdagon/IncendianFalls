using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainEffectVisitor {
  void visitTerrainCreateEffect(TerrainCreateEffect effect);
  void visitTerrainDeleteEffect(TerrainDeleteEffect effect);
  void visitTerrainSetPatternEffect(TerrainSetPatternEffect effect);
}

}
