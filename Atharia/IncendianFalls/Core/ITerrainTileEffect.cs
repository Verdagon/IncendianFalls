using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEffect : IEffect {
  int id { get; }
  void visitITerrainTileEffect(ITerrainTileEffectVisitor visitor);
}
       
}
