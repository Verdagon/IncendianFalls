using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IImpulse

        : IDestructible {
  IImpulse AsIImpulse();
  int GetWeight();
  Void Enact(Unit unit, Game game);
}
}
