using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlazeRodStrongMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public BlazeRodStrongMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public BlazeRodStrongMutSetIncarnation Copy() {
    return new BlazeRodStrongMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
