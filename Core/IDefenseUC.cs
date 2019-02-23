using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefenseUC

        : IUnitComponent {
  IDefenseUC AsIDefenseUC();
  int AffectIncomingDamage(int incomingDamage);
}
}
