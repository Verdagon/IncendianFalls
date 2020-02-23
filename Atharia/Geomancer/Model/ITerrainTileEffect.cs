using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainTileEffect {
  int id { get; }
  void visit(ITerrainTileEffectVisitor visitor);
}
       
}
