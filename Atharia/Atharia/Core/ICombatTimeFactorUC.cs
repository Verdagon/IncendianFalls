using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICombatTimeFactorUC {
  ICombatTimeFactorUC AsICombatTimeFactorUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(ICombatTimeFactorUC that);
  bool NullableIs(ICombatTimeFactorUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  int GetCombatTimeAddConstant();
  int GetCombatTimeMultiplierPercent();
  Void Destruct();
}
}
