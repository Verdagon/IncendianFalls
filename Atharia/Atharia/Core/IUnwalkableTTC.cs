using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnwalkableTTC {
  IUnwalkableTTC AsIUnwalkableTTC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IUnwalkableTTC that);
  bool NullableIs(IUnwalkableTTC that);
  IDestructible AsIDestructible();
  ITerrainTileComponent AsITerrainTileComponent();
  Void Destruct();
}
}
