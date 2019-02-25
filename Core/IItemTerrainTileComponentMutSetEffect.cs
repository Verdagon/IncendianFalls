using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTerrainTileComponentMutSetEffect {
  int id { get; }
  void visit(IItemTerrainTileComponentMutSetEffectVisitor visitor);
}

}
