using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargingUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public LightningChargingUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public LightningChargingUCMutSetIncarnation Copy() {
    return new LightningChargingUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
