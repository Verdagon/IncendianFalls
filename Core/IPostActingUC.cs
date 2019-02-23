using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPostActingUC

        : IUnitComponent {
  IPostActingUC AsIPostActingUC();
  Void PostAct(Unit unit);
}
}
