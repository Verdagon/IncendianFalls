using System;

using UnityEngine;

public static class Vector4Extensions {
  public static bool EqualsE(this Vector4 a, Vector4 b, float epsilon) {
    for (int i = 0; i < 4; i++) {
      if (Math.Abs(a[i] - b[i]) > epsilon) {
        return false;
      }
    }
    return true;
  }
}
