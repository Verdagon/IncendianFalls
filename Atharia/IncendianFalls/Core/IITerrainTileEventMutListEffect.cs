using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IITerrainTileEventMutListEffect {
  int id { get; }
  void visit(IITerrainTileEventMutListEffectVisitor visitor);
}

}
