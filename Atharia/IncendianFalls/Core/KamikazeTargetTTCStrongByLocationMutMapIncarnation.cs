using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeTargetTTCStrongByLocationMutMapIncarnation {
  public readonly SortedDictionary<Location, int> elements;

  public KamikazeTargetTTCStrongByLocationMutMapIncarnation(SortedDictionary<Location, int> elements) {
    this.elements = elements;
  }

  public KamikazeTargetTTCStrongByLocationMutMapIncarnation Copy() {
    return new KamikazeTargetTTCStrongByLocationMutMapIncarnation(new SortedDictionary<Location, int>(elements));
  }
}
         
}
