using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISightRangeFactorUC {
  ISightRangeFactorUC AsISightRangeFactorUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(ISightRangeFactorUC that);
  bool NullableIs(ISightRangeFactorUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  int GetSightRangeAddConstant();
  int GetSightRangeMultiplierPercent();
  Void Destruct();
}
}
