using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class IdentityVector4Animation : IVector4Animation {
    public Vector4 Get(long timeMs) {
      return new Vector4(0, 0, 0);
    }
    public IVector4Animation Simplify(long timeMs) {
      return this;
    }
  }
}