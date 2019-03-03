using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeTerrainTileComponentMutSetEffect {
  int id { get; }
  void visit(IDecorativeTerrainTileComponentMutSetEffectVisitor visitor);
}

}
