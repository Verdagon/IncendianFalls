using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefenseFactorUC {
  IDefenseFactorUC AsIDefenseFactorUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IDefenseFactorUC that);
  bool NullableIs(IDefenseFactorUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  int GetIncomingDamageAddConstant();
  int GetIncomingDamageMultiplierPercent();
  Void Destruct();
}
}
