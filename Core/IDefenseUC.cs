using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefenseUC {
  IDefenseUC AsIDefenseUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IDefenseUC that);
  bool NullableIs(IDefenseUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  int AffectIncomingDamage(int incomingDamage);
  Void Destruct();
}
}
