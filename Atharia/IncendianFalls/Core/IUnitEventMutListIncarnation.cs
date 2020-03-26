using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IUnitEventMutListIncarnation {
  public readonly List<IUnitEvent> list;

  public IUnitEventMutListIncarnation(List<IUnitEvent> list) {
    this.list = list;
  }

  public IUnitEventMutListIncarnation Copy() {
    return new IUnitEventMutListIncarnation(new List<IUnitEvent>(list));
  }
}
         
}
