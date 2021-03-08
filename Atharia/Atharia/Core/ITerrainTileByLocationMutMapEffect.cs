using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileByLocationMutMapEffect : IEffect {
  int id { get; }
  void visitITerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffectVisitor visitor);
}

}
