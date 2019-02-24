using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitComponent

        : IDestructible {
  IUnitComponent AsIUnitComponent();
}
}
