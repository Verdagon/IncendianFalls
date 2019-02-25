using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDirectiveUC {
  IDirectiveUC AsIDirectiveUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IDirectiveUC that);
  bool NullableIs(IDirectiveUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void Destruct();
}
}
