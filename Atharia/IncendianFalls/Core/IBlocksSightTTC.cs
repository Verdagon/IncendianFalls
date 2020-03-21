using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBlocksSightTTC {
  IBlocksSightTTC AsIBlocksSightTTC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IBlocksSightTTC that);
  bool NullableIs(IBlocksSightTTC that);
  IDestructible AsIDestructible();
  ITerrainTileComponent AsITerrainTileComponent();
  Void Destruct();
}
}
