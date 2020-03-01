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
  string GetName();
  bool ConsiderCornersAdjacent();
  Void SimpleTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName);
}
}
