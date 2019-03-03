using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IItemTerrainTileComponentEffect {
  int id { get; }
  void visit(IItemTerrainTileComponentEffectVisitor visitor);
}
       
}
