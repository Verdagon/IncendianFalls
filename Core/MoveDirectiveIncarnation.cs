using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class MoveDirectiveIncarnation {
  public int path;
  public MoveDirectiveIncarnation(
      int path) {
    this.path = path;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + path.GetDeterministicHashCode();
    return s;
  }
}

}
