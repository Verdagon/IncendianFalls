using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeTargetTTCStrongByLocationMutMapIncarnation {
  public readonly SortedDictionary<Location, int> map;

  public KamikazeTargetTTCStrongByLocationMutMapIncarnation(SortedDictionary<Location, int> map) {
    this.map = map;
  }

  public KamikazeTargetTTCStrongByLocationMutMapIncarnation Copy() {
    return new KamikazeTargetTTCStrongByLocationMutMapIncarnation(new SortedDictionary<Location, int>(map));
  }
}
         
}
