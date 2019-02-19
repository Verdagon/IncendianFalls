using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IFeature {
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IFeature that);
  bool NullableIs(IFeature that);
       }
}
