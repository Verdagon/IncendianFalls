using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlazeRodMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BlazeRodMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BlazeRodMutSetIncarnation Copy() {
    return new BlazeRodMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
