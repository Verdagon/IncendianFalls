using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainEffect {
  int id { get; }
  void visit(ITerrainEffectVisitor visitor);
}
       
}
