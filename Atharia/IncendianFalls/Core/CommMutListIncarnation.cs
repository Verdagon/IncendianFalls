using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommMutListIncarnation {
  public readonly List<int> list;

  public CommMutListIncarnation(List<int> list) {
    this.list = list;
  }

  public CommMutListIncarnation Copy() {
    return new CommMutListIncarnation(new List<int>(list));
  }
}
         
}
