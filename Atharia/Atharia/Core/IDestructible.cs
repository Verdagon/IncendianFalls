using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDestructible {
  IDestructible AsIDestructible();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IDestructible that);
  bool NullableIs(IDestructible that);
  Void Destruct();
}
}
