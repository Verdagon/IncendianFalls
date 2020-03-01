using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPresenceTriggerTTC {
  IPresenceTriggerTTC AsIPresenceTriggerTTC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IPresenceTriggerTTC that);
  bool NullableIs(IPresenceTriggerTTC that);
  IDestructible AsIDestructible();
  ITerrainTileComponent AsITerrainTileComponent();
  Void Trigger(Game game, Superstate superstate, Unit triggeringUnit, Location containingTileLocation);
  Void Destruct();
}
}
