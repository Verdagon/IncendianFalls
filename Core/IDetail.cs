using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDetail {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IDetail that);
  bool NullableIs(IDetail that);
         int AffectIncomingDamage(int incomingDamage);
  Void PreAct(Unit unit);
  Void PostAct(Unit unit);
}
}
