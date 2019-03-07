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
  Location GetEntryLocation(Game game, LevelSuperstate levelSuperstate, Level fromLevel, int fromLevelPortalIndex);
}
}
