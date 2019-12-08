using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityFloatAnimation : IFloatAnimation {
  public float Get(float time) {
    return 0;
  }

  public IFloatAnimation Simplify(float time) {
    return this;
  }
}
