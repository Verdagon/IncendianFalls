using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefenseUnitComponent {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IDefenseUnitComponent that);
  bool NullableIs(IDefenseUnitComponent that);
         int AffectIncomingDamage(int incomingDamage);
}
}
