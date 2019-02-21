using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDownStaircaseTerrainTileComponentEffect {
  int id { get; }
  void visit(IDownStaircaseTerrainTileComponentEffectVisitor visitor);
}
       
}
