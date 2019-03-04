using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOperationUC {
  IOperationUC AsIOperationUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IOperationUC that);
  bool NullableIs(IOperationUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void BeforeImpulse(Game game, Superstate superstate, Unit unit, IImpulse impulse);
  Void Destruct();
}
}
