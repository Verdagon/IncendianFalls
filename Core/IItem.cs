using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IItem {
  IItem AsIItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IItem that);
  bool NullableIs(IItem that);
  int AffectIncomingDamage(int incomingDamage);
  int AffectOutgoingDamage(int outgoingDamage);
}
}
