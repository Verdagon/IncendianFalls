using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITerrainTileWeakMutSetEffect : IEffect {
  int id { get; }
  void visitITerrainTileWeakMutSetEffect(ITerrainTileWeakMutSetEffectVisitor visitor);
}

}
