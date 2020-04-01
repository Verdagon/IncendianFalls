using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommMutListIncarnation {
  public readonly List<int> elements;

  public CommMutListIncarnation(List<int> elements) {
    this.elements = elements;
  }

  public CommMutListIncarnation Copy() {
    return new CommMutListIncarnation(new List<int>(elements));
  }
}
         
}
