using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LocationMutListIncarnation {
  public readonly List<Location> list;

  public LocationMutListIncarnation(List<Location> list) {
    this.list = list;
  }

  public LocationMutListIncarnation Copy() {
    return new LocationMutListIncarnation(new List<Location>(list));
  }
}
         
}
