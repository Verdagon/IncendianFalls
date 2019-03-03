using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDecorativeTerrainTileComponentEffect {
  int id { get; }
  void visit(IDecorativeTerrainTileComponentEffectVisitor visitor);
}
       
}
