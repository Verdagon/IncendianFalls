using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainTileWeakMutSetEffect {
  int id { get; }
  void visit(ITerrainTileWeakMutSetEffectVisitor visitor);
}

}
