using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class StrMutListIncarnation {
  public readonly List<string> list;

  public StrMutListIncarnation(List<string> list) {
    this.list = list;
  }

  public StrMutListIncarnation Copy() {
    return new StrMutListIncarnation(new List<string>(list));
  }
}
         
}
