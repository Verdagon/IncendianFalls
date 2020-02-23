using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityMatrix4x4Animation : IMatrix4x4Animation {
  public Matrix4x4 Get(long timeMs) {
    return Matrix4x4.identity;
  }
  public IMatrix4x4Animation Simplify(long timeMs) {
    return this;
  }
}
