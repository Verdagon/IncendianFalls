using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityFloatAnimation : IFloatAnimation {
  public float Get(long timeMs) {
    return 0;
  }

  public IFloatAnimation Simplify(long timeMs) {
    return this;
  }
}
