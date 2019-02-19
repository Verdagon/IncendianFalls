using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class DecorativeFeatureIncarnation {
  public string symbolId;
  public DecorativeFeatureIncarnation(
      string symbolId) {
    this.symbolId = symbolId;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + symbolId.GetDeterministicHashCode();
    return s;
  }
}

}
