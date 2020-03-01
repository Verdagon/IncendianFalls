using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SimplePresenceTriggerTTCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public SimplePresenceTriggerTTCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
