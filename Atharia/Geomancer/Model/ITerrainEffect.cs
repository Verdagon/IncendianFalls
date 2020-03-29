using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ITerrainEffect : IEffect {
  int id { get; }
  void visitITerrainEffect(ITerrainEffectVisitor visitor);
}
       
}
