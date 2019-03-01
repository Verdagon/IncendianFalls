using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BidingOperationUCMutSetIncarnation {
  public readonly SortedSet<int> set;

  public BidingOperationUCMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }
}

}
