using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityVector3Animation : IVector3Animation {
  public Vector3 Get(long timeMs) {
    return new Vector3(0, 0, 0);
  }
  public IVector3Animation Simplify(long timeMs) {
    return this;
  }
}
