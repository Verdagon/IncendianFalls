using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackAICapabilityUCWeakMutSetIncarnation {
  public readonly SortedSet<int> set;

  public AttackAICapabilityUCWeakMutSetIncarnation(SortedSet<int> set) {
    this.set = new SortedSet<int>(set);
  }

  //public int GetDeterministicHashCode() {
  //  int hash = 0;
  //  hash = hash * 37 + set.Count;
  //  foreach (var element in set) {
  //    hash = hash * 37 + element.GetDeterministicHashCode();
  //  }
  // return hash;
  //}
}

}
