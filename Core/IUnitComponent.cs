using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitComponent {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IUnitComponent that);
  bool NullableIs(IUnitComponent that);
       }
}
