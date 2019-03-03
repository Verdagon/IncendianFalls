using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IITerrainTileComponentMutBunchEffect {
  int id { get; }
  void visit(IITerrainTileComponentMutBunchEffectVisitor visitor);
}
       
}
