using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MagmaTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public MagmaTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public MagmaTTCMutSetIncarnation Copy() {
    return new MagmaTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
