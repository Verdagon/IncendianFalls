using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LocationMutListIncarnation {
  public readonly List<Location> elements;

  public LocationMutListIncarnation(List<Location> elements) {
    this.elements = elements;
  }

  public LocationMutListIncarnation Copy() {
    return new LocationMutListIncarnation(new List<Location>(elements));
  }
}
         
}
