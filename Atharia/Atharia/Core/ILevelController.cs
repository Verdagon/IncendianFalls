using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelController {
  ILevelController AsILevelController();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(ILevelController that);
  bool NullableIs(ILevelController that);
  IDestructible AsIDestructible();
  Void Destruct();
  string GetName();
  Void SimpleTrigger(IncendianFalls.SSContext context, Game game, Superstate superstate, string triggerName);
  Void SimpleUnitTrigger(IncendianFalls.SSContext context, Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName);
}
}
