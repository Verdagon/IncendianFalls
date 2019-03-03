using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseTerrainTileComponentMutSetEffect {
  int id { get; }
  void visit(IDownStaircaseTerrainTileComponentMutSetEffectVisitor visitor);
}

}
