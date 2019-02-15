using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDirective {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IDirective that);
  bool NullableIs(IDirective that);
       }
}
