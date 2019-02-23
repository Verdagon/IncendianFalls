using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOperationUC

        : IUnitComponent {
  IOperationUC AsIOperationUC();
  Void OnImpulse(Unit unit, Game game, IImpulse impulse);
}
}
