using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseTerrainTileComponentMutSetEffect {
  int id { get; }
  void visit(IUpStaircaseTerrainTileComponentMutSetEffectVisitor visitor);
}

}
