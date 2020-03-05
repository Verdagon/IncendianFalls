using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOffenseFactorUC {
  IOffenseFactorUC AsIOffenseFactorUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IOffenseFactorUC that);
  bool NullableIs(IOffenseFactorUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  int GetOutgoingDamageAddConstant();
  int GetOutgoingDamageMultiplierPercent();
  Void Destruct();
}
}
