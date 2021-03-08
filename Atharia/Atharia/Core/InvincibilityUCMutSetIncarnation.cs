using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InvincibilityUCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public InvincibilityUCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public InvincibilityUCMutSetIncarnation Copy() {
    return new InvincibilityUCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
