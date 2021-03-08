using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPickUpReactorItem {
  IPickUpReactorItem AsIPickUpReactorItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IPickUpReactorItem that);
  bool NullableIs(IPickUpReactorItem that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IItem AsIItem();
  Void ReactToPickUp(Game game, Superstate superstate, Unit unit);
  Void Destruct();
}
}
