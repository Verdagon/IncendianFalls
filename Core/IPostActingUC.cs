using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPostActingUC {
  IPostActingUC AsIPostActingUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IPostActingUC that);
  bool NullableIs(IPostActingUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void PostAct(Unit unit);
  Void Destruct();
}
}
