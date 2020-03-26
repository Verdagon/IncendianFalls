using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IITerrainTileEventMutListEffect : IEffect {
  int id { get; }
  void visitIITerrainTileEventMutListEffect(IITerrainTileEventMutListEffectVisitor visitor);
}

}
