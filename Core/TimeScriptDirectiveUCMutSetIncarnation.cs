using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeScriptDirectiveUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public TimeScriptDirectiveUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
