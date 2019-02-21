using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IPreActingUnitComponent {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IPreActingUnitComponent that);
  bool NullableIs(IPreActingUnitComponent that);
         Void PreAct(Unit unit);
}
}
