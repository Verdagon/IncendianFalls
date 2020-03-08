using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargingUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public LightningChargingUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
