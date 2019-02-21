using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IPostActingUnitComponent {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IPostActingUnitComponent that);
  bool NullableIs(IPostActingUnitComponent that);
         Void PostAct(Unit unit);
}
}
