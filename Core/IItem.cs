using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IItem {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IItem that);
  bool NullableIs(IItem that);
         int AffectIncomingDamage(int incomingDamage);
  int AffectOutgoingDamage(int outgoingDamage);
}
}
