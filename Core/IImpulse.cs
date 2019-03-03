using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IImpulse {
  IImpulse AsIImpulse();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IImpulse that);
  bool NullableIs(IImpulse that);
  IDestructible AsIDestructible();
  int GetWeight();
  bool Enact(Game game, Superstate superstate, Unit unit);
  Void Destruct();
}
}
