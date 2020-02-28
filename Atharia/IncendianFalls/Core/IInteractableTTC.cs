using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IInteractableTTC {
  IInteractableTTC AsIInteractableTTC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IInteractableTTC that);
  bool NullableIs(IInteractableTTC that);
  IDestructible AsIDestructible();
  ITerrainTileComponent AsITerrainTileComponent();
  string Interact(Game game, Superstate superstate, Unit interactingUnit, Location containingTileLocation);
  Void Destruct();
}
}
