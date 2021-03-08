using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDeathPreReactor {
  IDeathPreReactor AsIDeathPreReactor();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IDeathPreReactor that);
  bool NullableIs(IDeathPreReactor that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void BeforeDeath(Game game, Superstate superstate, Unit unit);
  Void Destruct();
}
}
