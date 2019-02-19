using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class DefendingDetailIncarnation {
  public int power;
  public DefendingDetailIncarnation(
      int power) {
    this.power = power;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + power.GetDeterministicHashCode();
    return s;
  }
}

}
