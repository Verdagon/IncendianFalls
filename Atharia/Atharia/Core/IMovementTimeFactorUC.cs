using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMovementTimeFactorUC {
  IMovementTimeFactorUC AsIMovementTimeFactorUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IMovementTimeFactorUC that);
  bool NullableIs(IMovementTimeFactorUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  int GetMovementTimeAddConstant();
  int GetMovementTimeMultiplierPercent();
  Void Destruct();
}
}
