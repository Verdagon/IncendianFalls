using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class RandIncarnation {
  public int rand;
  public RandIncarnation(
      int rand) {
    this.rand = rand;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + rand.GetDeterministicHashCode();
    return s;
  }
}

}
