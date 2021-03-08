using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IITerrainTileComponentMutBunchEffect : IEffect {
  int id { get; }
  void visitIITerrainTileComponentMutBunchEffect(IITerrainTileComponentMutBunchEffectVisitor visitor);
}
       
}
