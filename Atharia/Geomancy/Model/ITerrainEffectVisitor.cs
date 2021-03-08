using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public interface ITerrainEffectVisitor {
  void visitTerrainCreateEffect(TerrainCreateEffect effect);
  void visitTerrainDeleteEffect(TerrainDeleteEffect effect);
}

}
