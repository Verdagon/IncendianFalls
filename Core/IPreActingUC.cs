using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPreActingUC

        : IUnitComponent {
  IPreActingUC AsIPreActingUC();
  Void PreAct(Game game, Unit unit);
}
}
