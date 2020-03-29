using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainTileEffect : IEffect {
  int id { get; }
  void visitITerrainTileEffect(ITerrainTileEffectVisitor visitor);
}
       
}
