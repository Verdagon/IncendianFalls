using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InvincibilityUCWeakMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public InvincibilityUCWeakMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public InvincibilityUCWeakMutSetIncarnation Copy() {
    return new InvincibilityUCWeakMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
