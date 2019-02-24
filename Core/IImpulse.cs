using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IImpulse {
  IImpulse AsIImpulse();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  bool Is(IImpulse that);
  bool NullableIs(IImpulse that);
  IDestructible AsIDestructible();
  int GetWeight();
  Void Enact(Unit unit, Game game);
  Void Destruct();
}
}
