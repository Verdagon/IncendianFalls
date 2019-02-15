using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileEffect {
  int id { get; }
  void visit(ITerrainTileEffectVisitor visitor);
}
       
}
