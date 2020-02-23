using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainTileByLocationMutMapEffect {
  int id { get; }
  void visit(ITerrainTileByLocationMutMapEffectVisitor visitor);
}

}
