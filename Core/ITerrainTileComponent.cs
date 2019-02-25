using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITerrainTileComponent {
  ITerrainTileComponent AsITerrainTileComponent();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(ITerrainTileComponent that);
  bool NullableIs(ITerrainTileComponent that);
  IDestructible AsIDestructible();
  Void Destruct();
}
}
