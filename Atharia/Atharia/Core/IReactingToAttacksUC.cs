using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IReactingToAttacksUC {
  IReactingToAttacksUC AsIReactingToAttacksUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IReactingToAttacksUC that);
  bool NullableIs(IReactingToAttacksUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  bool React(Game game, Superstate superstate, Unit unit, Unit attacker);
  Void Destruct();
}
}
