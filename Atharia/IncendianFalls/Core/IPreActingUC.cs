using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPreActingUC {
  IPreActingUC AsIPreActingUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IPreActingUC that);
  bool NullableIs(IPreActingUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void PreAct(Game game, Superstate superstate, Unit unit);
  Void Destruct();
}
}
