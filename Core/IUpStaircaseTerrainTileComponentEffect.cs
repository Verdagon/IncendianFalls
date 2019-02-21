using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUpStaircaseTerrainTileComponentEffect {
  int id { get; }
  void visit(IUpStaircaseTerrainTileComponentEffectVisitor visitor);
}
       
}
