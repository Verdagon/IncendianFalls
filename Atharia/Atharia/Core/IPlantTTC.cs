using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPlantTTC {
  IPlantTTC AsIPlantTTC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IPlantTTC that);
  bool NullableIs(IPlantTTC that);
  IDestructible AsIDestructible();
  ITerrainTileComponent AsITerrainTileComponent();
  Void Destruct();
}
}
