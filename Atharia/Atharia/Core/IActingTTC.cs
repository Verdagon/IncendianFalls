using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IActingTTC {
  IActingTTC AsIActingTTC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IActingTTC that);
  bool NullableIs(IActingTTC that);
  IDestructible AsIDestructible();
  ITerrainTileComponent AsITerrainTileComponent();
  Void Act(Game game, Superstate superstate, Location containingTileLocation);
  Void Destruct();
}
}
